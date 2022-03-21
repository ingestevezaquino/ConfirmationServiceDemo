﻿using CSDiagADSL.Services.Job;
using Microsoft.AspNetCore.Mvc;

namespace CSDiagADSL.Controllers
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
        public async Task<IActionResult> Exec()
        {
            await _jobService.Exec();
            return Ok();
        }
    }
}
