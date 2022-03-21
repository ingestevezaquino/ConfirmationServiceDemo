using AutoMapper;
using CSEAD.API.DTOs;
using CSEAD.Services.Diagnostics;
using CSEAD.Services.Facilities;
using Microsoft.AspNetCore.Mvc;

namespace CSEAD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticsController : ControllerBase
    {
        private readonly IDiagnosticService _diagnosticService;
        private readonly IMapper _mapper;

        public DiagnosticsController(IDiagnosticService diagnosticService, IMapper mapper)
        {
            _diagnosticService = diagnosticService;
            _mapper = mapper;
        }

        [HttpGet("{subscriberNumber}")]
        public async Task<IActionResult> GetDiagnosticsByPhoneNumber(string subscriberNumber)
        {
            var result = _mapper.Map<DiagnosticDto>(await _diagnosticService.CarryOutDiagnostics(subscriberNumber));
            return Ok(result);
        }
    }
}
