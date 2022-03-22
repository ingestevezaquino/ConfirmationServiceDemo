using CSCore.Services.Job;
using Microsoft.AspNetCore.Mvc;

namespace CSCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
           _jobService = jobService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok($"Amount of tickets that were loaded [{await _jobService.LoadTickets()}].");
        }
    }
}
