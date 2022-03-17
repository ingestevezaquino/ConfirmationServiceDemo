using CSCore.Persistence.Models;
using CSCore.Services.Job;
using CSCore.Tests.Shared;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CSCore.Tests.Services
{
    public class JobServiceTests : DemoTicketsDBFixture
    {
        public JobServiceTests()
        {
            CreateDBMockData();
        }

        [Fact]
        public async Task LoadTickets_InsertsNewTicketsIntoTicketsTable_TicketsTableHasMoreThanTwoTickets()
        {
            // Arrange
            // 
            Mock<HttpClient> httpClientMock = new Mock<HttpClient>();

            Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(httpClientMock.Object);

            IJobService jobService = new JobService(DbContext, httpClientFactoryMock.Object);

            // Act
            //
            int totalNewTickets = await jobService.LoadTickets();
            int totalAmountOfTickets = (await DbContext.Tickets.ToListAsync()).Count();

            // Asert
            //
            Assert.True(totalAmountOfTickets > 2);        
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

        #endregion
    }
}