using CSCore.Persistence;
using CSCore.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace CSCore.Services.Tickets
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDBContext _context;

        public TicketService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllTickets()
        {
            List<Ticket> tickets = await _context.Tickets.ToListAsync();
            return tickets;
        }
    }
}
