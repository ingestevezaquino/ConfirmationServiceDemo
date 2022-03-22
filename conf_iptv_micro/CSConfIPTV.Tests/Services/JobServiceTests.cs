using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using CSConfIPTV.Tests.Shared;
using CSConfIPTV.Services.Job;
using CSConfIPTV.Services.Utils;
using CSConfIPTV.Persistence.Models;
using CSConfIPTV.Persistence;

namespace CSConfIPTV.Tests.Services
{
    public class JobServiceTests : ApplicationDBFixture
    {
        public JobServiceTests()
        {
            CreateDBMockData();
        }

        [Fact]
        public async Task Exec_ExecutesConfIPTVProcess_DoesNotFindPendingTickets()
        {
            // Arrange
            // 
            IJobService jobService = CreateJobService();

            // Act
            //
            await jobService.Exec();
            var tickets = await DbContext.Tickets
                .Where(t => t.ProcessName == CommonValues.PROCESS_NAME)
                .Where(t => t.Status == CommonValues.TICKET_STATUS)
                .OrderBy(t => t.CreationTime)
                .ToListAsync();

            // Asert
            //
            Assert.Empty(tickets);        
        }

        #region Private Methods
        
        private void CreateDBMockData()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();

            List<Ticket> newTickets = new()
            {
                new Ticket()
                {
                    ProcessName = "DIAG-ADSL",
                    ProductType = "ADSL",
                    CaseNumber = "356241",
                    SubscriberNumber = "3545558888",
                    CurrentQueue = "ADSL FAULT DIAGNOSIS",
                    UAC = "243",
                    ClientName = "Bailey Davidson",
                    ClientContactPhone = "4607295853",
                    Status = "PENDING",
                },

                new Ticket
                {
                    ProcessName = "CONF-IPTV",
                    ProductType = "IPTV",
                    CaseNumber = "142653",
                    SubscriberNumber = "3546661515",
                    CurrentQueue = "IPTV FAULT CONFIRMATION",
                    UAC = "789",
                    ClientName = "Remi Holmes",
                    ClientContactPhone = "16967598718",
                    Status = "PENDING",
                }
            };

            List<Process> newProcesses = new()
            {
                new Process()
                {
                    Name = "DIAG-ADSL",
                    Description = "Check configurations and params for tickets that have ADSL as a product in the diagnostic stage",
                    IsActive = true,
                },

                new Process()
                {
                    Name = "CONF-IPTV",
                    Description = "Check configurations and params for tickets that have IPTV as a product in the confirmation stage",
                    IsActive = true,
                }
            };

            DbContext.Tickets.AddRange(newTickets);
            DbContext.Processes.AddRange(newProcesses);
            DbContext.SaveChanges();
        }

        private IJobService CreateJobService()
        {
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient());

            var loggerMock = new Mock<ILogger<JobService>>();

            #region CONFIGURE DI

            var serviceCollection = new ServiceCollection();

            GetInMemoryTicketsDBContext();

            // Add any DI stuff here:
            serviceCollection.AddTransient<ApplicationDBContext>(x => GetInMemoryTicketsDBContext());

            // Create the ServiceProvider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // serviceScopeMock will contain my ServiceProvider
            var serviceScopeMock = new Mock<IServiceScope>();
            serviceScopeMock.SetupGet<IServiceProvider>(s => s.ServiceProvider)
                .Returns(serviceProvider);

            // serviceScopeFactoryMock will contain my serviceScopeMock
            var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
            serviceScopeFactoryMock.Setup(s => s.CreateScope())
                .Returns(serviceScopeMock.Object);

            #endregion

            return new JobService(DbContext, httpClientFactoryMock.Object, serviceScopeFactoryMock.Object, loggerMock.Object);
        }

        #endregion
    }
}