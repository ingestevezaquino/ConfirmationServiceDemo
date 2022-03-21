using CSCore.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCore.Services.Tickets
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTickets();
    }
}
