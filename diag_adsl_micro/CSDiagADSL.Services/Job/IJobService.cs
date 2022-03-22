using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSDiagADSL.Services.Job
{
    public interface IJobService
    {
        Task Exec();
    }
}
