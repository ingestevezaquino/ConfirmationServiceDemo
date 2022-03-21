using CSEAD.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEAD.Services.Facilities
{
    public interface IFacilityService
    {
        Task GenerateFacilitiesForSubscriberNumber(string subscriberNumber);
    }
}
