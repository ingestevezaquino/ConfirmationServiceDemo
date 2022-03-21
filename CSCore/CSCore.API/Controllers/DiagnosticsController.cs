using CSCore.Services.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CSCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticsController : ControllerBase
    {
        private readonly IDiagnosticService _diagnosticService;

        public DiagnosticsController(IDiagnosticService diagnosticService)
        {
            _diagnosticService = diagnosticService;
        }

        [HttpGet("{subscriberNumber}")]
        public async Task<IActionResult> GetLastDiagnosticAndFacility(string subscriberNumber)
        {
            return Ok(await _diagnosticService.GetLastDiagnosisAndFacility(subscriberNumber));
        }
    }
}
