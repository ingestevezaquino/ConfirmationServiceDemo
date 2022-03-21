using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEAD.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDiagnosticRepository Diagnostics { get; }
        IFacilityRepository Facilities { get; }
        Task<int> Complete();
    }
}
