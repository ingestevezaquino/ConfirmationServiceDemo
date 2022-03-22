using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCore.Services.Job
{
    public interface IJobService
    {
        Task<int> LoadTickets();
    }
}
