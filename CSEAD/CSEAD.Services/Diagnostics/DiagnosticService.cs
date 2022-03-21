using CSEAD.Persistence;
using CSEAD.Persistence.Interfaces;
using CSEAD.Persistence.Models;
using CSEAD.Services.Facilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEAD.Services.Diagnostics
{
    public class DiagnosticService : IDiagnosticService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFacilityService _facilityService;

        public DiagnosticService(IUnitOfWork unitOfWork, IFacilityService facilityService)
        {
            _unitOfWork = unitOfWork;
            _facilityService = facilityService;
        }

        public async Task<Diagnostic> CarryOutDiagnostics(string subscriberNumber)
        {
            Facility facility = await _unitOfWork.Facilities.FindSingleAsync(f => f.SubscriberNumber == subscriberNumber);
            if (facility == null)
            {
                await _facilityService.GenerateFacilitiesForSubscriberNumber(subscriberNumber);
                facility = await _unitOfWork.Facilities.FindSingleAsync(f => f.SubscriberNumber == subscriberNumber);
            }

            Random rand = new();
            Diagnostic diagnostic = null;

            if (rand.Next(0, 9) == 9)
            {
                diagnostic = new()
                {
                    FacilityId = facility.Id,
                    IsConfigured = true,
                    OLTAdminState = true,
                    OLTOperState = true,
                    ONTAdminState = true,
                    ONTOperState = true,
                    ONTRxPower = true,
                    ONTTxPower = true,
                    ONTVoltage = true,
                    Facility = facility,
                };
            } 
            else
            {
                diagnostic = new()
                {
                    FacilityId = facility.Id,
                    IsConfigured = Convert.ToBoolean(rand.Next(0, 2)),
                    OLTAdminState = Convert.ToBoolean(rand.Next(0, 2)),
                    OLTOperState = Convert.ToBoolean(rand.Next(0, 2)),
                    ONTAdminState = Convert.ToBoolean(rand.Next(0, 2)),
                    ONTOperState = Convert.ToBoolean(rand.Next(0, 2)),
                    ONTRxPower = Convert.ToBoolean(rand.Next(0, 2)),
                    ONTTxPower = Convert.ToBoolean(rand.Next(0, 2)),
                    ONTVoltage = Convert.ToBoolean(rand.Next(0, 2)),
                    Facility = facility,
                };
            }

            await _unitOfWork.Diagnostics.AddAsync(diagnostic);
            await _unitOfWork.Complete();

            return diagnostic;
        }
    }
}
