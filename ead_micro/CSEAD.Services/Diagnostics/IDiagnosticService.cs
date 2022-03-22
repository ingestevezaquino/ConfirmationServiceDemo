using CSEAD.Persistence.Models;

namespace CSEAD.Services.Diagnostics
{
    public interface IDiagnosticService
    {
        Task<Diagnostic> GetFacilityLastDiagnosticBySubscriberNumber(string susbcriberNumber);
        Task<Diagnostic> CarryOutDiagnostics(string susbcriberNumber);
    }
}
