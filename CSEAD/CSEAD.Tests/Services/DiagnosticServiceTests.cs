using CSEAD.Persistence;
using CSEAD.Persistence.Interfaces;
using CSEAD.Persistence.Models;
using CSEAD.Services.Diagnostics;
using CSEAD.Services.Facilities;
using CSEAD.Tests.Shared;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CSEAD.Servuces.Tests
{
    public class DiagnosticServiceTests : ApplicationDBFixture
    {
        public DiagnosticServiceTests()
        {
            CreateDBMockData();
        }

        [Fact]
        public async Task CarryOutDiagnostics_SearchFacilityAndCarryOutDiagnostics_ReturnsNotNullDiagnostics()
        {
            // Arrange
            // 
            IUnitOfWork unitOfWork = new UnitOfWork(DbContext);
            IFacilityService facilityService = new FacilityService(unitOfWork);
            IDiagnosticService diagnosticService = new DiagnosticService(unitOfWork, facilityService);

            // Act
            //
            var diagnostic = await diagnosticService.CarryOutDiagnostics("3545558888");

            // Asert
            //
            Assert.NotNull(diagnostic);
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
