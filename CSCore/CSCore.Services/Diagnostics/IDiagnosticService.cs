using CSCore.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCore.Services.Diagnostics
{
    public interface IDiagnosticService
    {
        Task<ClientResponse.Diagnostic> GetLastDiagnosisAndFacility(string subcriberNumber);
    }
}
