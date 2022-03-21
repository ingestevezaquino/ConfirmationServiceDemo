using CSEAD.Persistence.Interfaces;
using CSEAD.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEAD.Services.Facilities
{
    public class FacilityService : IFacilityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FacilityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task GenerateFacilitiesForSubscriberNumber(string SubscriberNumber)
        {
            Random rand = new();

            Facility facility = new()
            {
                SubscriberNumber = SubscriberNumber,
                NodeAddress = $"GEQP-{rand.Next(10, 21)}-{rand.Next(10, 21)}-{rand.Next(1, 10)}",
                IpAddress = $"{rand.Next(110, 200)}.{rand.Next(110, 170)}.{rand.Next(10, 100)}.{rand.Next(100, 110)}",
                NodeName = $"REPDOM2022",
            };

            await _unitOfWork.Facilities.AddAsync(facility);
            await _unitOfWork.Complete();
        }
    }
}
