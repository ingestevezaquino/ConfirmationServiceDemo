using System;
using System.Collections.Generic;

namespace CSCore.Persistence.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Diagnostics = new HashSet<Diagnostic>();
            Facilities = new HashSet<Facility>();
        }

        public int Id { get; set; }
        public string ProcessName { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public string CaseNumber { get; set; } = null!;
        public string SubscriberNumber { get; set; } = null!;
        public string CurrentQueue { get; set; } = null!;
        public string? DestinationQueue { get; set; }
        public string UAC { get; set; } = null!;
        public string ClientName { get; set; } = null!;
        public string ClientContactPhone { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public string Username { get; set; } = null!;

        public virtual ICollection<Diagnostic> Diagnostics { get; set; }
        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
