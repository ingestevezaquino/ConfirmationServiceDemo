using CSCore.Persistence;
using CSCore.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CSCore.Services.Job
{
    public class JobService : IJobService
    {
        private readonly ApplicationDBContext _context;
        private readonly HttpClient _httpClient;
        private readonly ILogger<JobService> _logger;
        private readonly Random _random;

        public JobService(ApplicationDBContext context, IHttpClientFactory httpClientFactory, ILogger<JobService> logger)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient("RandomNamesRestClient");
            _httpClient.BaseAddress = new Uri("http://names.drycodes.com/");
            _logger = logger;
            _random = new Random();
        }

        public async Task<int> LoadTickets()
        {
            _logger.LogInformation("START PROCESS - LOAD TICKETS");
            int amountOfNewTickets = _random.Next(20);

            List<Ticket> tickets = new();
            List<Process> processes = await _context.Processes.ToListAsync();

            for (int i = 0; i < amountOfNewTickets; i++)
            {
                tickets.Add(await GenerateRandomTicket(processes[_random.Next(0, 2)].Name));
            }

            // Simulate we are fetching ticket's information from another system, a CRM for instance.
            await Task.Delay(5000);

            await _context.Tickets.AddRangeAsync(tickets);
            _logger.LogInformation($"A TOTAL OF {amountOfNewTickets} NEW TICKETS WERE LOADED");
            return await _context.SaveChangesAsync();
        }

        private async Task<Ticket> GenerateRandomTicket(string processName)
        {
            Ticket ticket = new()
            {
                ProcessName = processName,
                ProductType = processName.Split("-")[1],
                CaseNumber = await GenerateRandomUniqueCaseNumber(),
                Status = "PENDING",
                UAC = await GenerateRandomUniqueAccountCode()
            };

            for (int i = 0; i < _random.Next(5, 7); i++)
            {
                ticket.SubscriberNumber += _random.Next(10, 100).ToString();
                ticket.ClientContactPhone += _random.Next(10, 100).ToString();
            }

            ticket.CurrentQueue = $"{ticket.ProductType}";
            if (processName.Contains("DIAG")) ticket.CurrentQueue += " FAULT DIAGNOSIS";
            else ticket.CurrentQueue += " FAULT CONFIRMATION";

            string listOfRandomNamesInJSON = await _httpClient.GetStringAsync("10?nameOptions=boy_names");
            List<string> names = JsonConvert.DeserializeObject<List<string>>(listOfRandomNamesInJSON);
            ticket.ClientName = names[_random.Next(0, 10)];
            if (ticket.ClientName.Contains('_')) ticket.ClientName.Replace("_", " ");
            return ticket;
        }

        private async Task<string> GenerateRandomUniqueCaseNumber()
        {
            string caseNumber = null;
            bool isUnique = false;
            while (!isUnique)
            {
                caseNumber = _random.Next(10000, 50000).ToString();
                isUnique = !(await _context.Tickets.Where(t => t.CaseNumber == caseNumber).AnyAsync());
            }
            return caseNumber;
        }

        private async Task<string> GenerateRandomUniqueAccountCode()
        {
            string uniqueAccountCode = null;
            bool isUnique = false;
            while (!isUnique)
            {
                uniqueAccountCode = _random.Next(1, 100000).ToString();
                isUnique = !(await _context.Tickets.Where(t => t.UAC == uniqueAccountCode).AnyAsync());
            }
            return uniqueAccountCode;
        }

    }
}