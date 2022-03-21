using CSDiagADSL.Persistence;
using CSDiagADSL.Services.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CSDiagADSL.Services.Job
{
    public class JobService : IJobService
    {
        private readonly ApplicationDBContext _context;
        private readonly HttpClient _httpClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<JobService> _logger;

        public JobService(ApplicationDBContext context, 
            IHttpClientFactory httpClientFactory,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<JobService> logger)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient("EADRestClient");
            _httpClient.BaseAddress = new Uri("http://equipments-diagnostics-micro/");
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task Exec()
        {
            var tickets = await _context.Tickets
                .Where(t => t.ProcessName == CommonValues.PROCESS_NAME)
                .Where(t => t.Status == CommonValues.TICKET_STATUS)
                .OrderBy(t => t.CreationTime)
                .ToListAsync();

            _logger.LogInformation($"STARTING PROCESS {CommonValues.PROCESS_NAME} FOR A TOTAL OF {tickets.Count} TICKETS ...");

            await Parallel.ForEachAsync(tickets, new ParallelOptions { MaxDegreeOfParallelism = 5 }, async (ticket, token) =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetService<ApplicationDBContext>();
                context.Tickets.Attach(ticket);

                string response = await _httpClient.GetStringAsync($"api/diagnostics/{ticket.SubscriberNumber}");
                var diagnostic = JsonConvert.DeserializeObject<ClientResponse.Diagnostic>(response);

                // Move ticket to corresponding queue
                // Then change its status in our table

                if (diagnostic is null)
                {
                    ticket.Status = "REFERRED TO MANUAL QUEUE / COULDN'T GET TICKET DIAGNOSTIC";
                    await context.SaveChangesAsync();
                    return;
                }

                #region CHECK CONFIGURATION

                if (!diagnostic.IsConfigured)
                {
                    ticket.Status = "REFERRED TO TECHNICIAN QUEUE / BAD CONFIGURATION";
                    await context.SaveChangesAsync();
                    return;
                }

                #endregion

                #region CHECK SYNC

                if (!diagnostic.OLTAdminState || !diagnostic.OLTOperState || !diagnostic.ONTAdminState || !diagnostic.ONTOperState)
                {
                    ticket.Status = "REFERRED TO MANUAL QUEUE / NO SYNC";
                    await context.SaveChangesAsync();
                    return;
                }

                #endregion

                #region CHECK PARAMS

                if (!diagnostic.ONTRxPower || !diagnostic.ONTTxPower || !diagnostic.ONTVoltage)
                {
                    ticket.Status = "REFERRED TO TECHNICIAN QUEUE / PARAMS";
                    await context.SaveChangesAsync();
                    return;
                }

                #endregion

                ticket.Status = "REFERRED TO MANUAL QUEUE / ALL OK";
                await context.SaveChangesAsync();
            });

            _logger.LogInformation($"END PROCESS {CommonValues.PROCESS_NAME} ...");
        }
    }
}