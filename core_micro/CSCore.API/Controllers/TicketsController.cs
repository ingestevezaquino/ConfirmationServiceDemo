using CSCore.Services.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace CSCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        
        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            return Ok(await _ticketService.GetAllTickets());
        }
    }
}
