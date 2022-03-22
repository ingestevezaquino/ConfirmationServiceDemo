using System;
using System.Collections.Generic;

namespace CSCore.Persistence.Models
{
    public partial class Facility
    {
        public Facility()
        {
            Diagnostics = new HashSet<Diagnostic>();
        }

        public int Id { get; set; }
        public string SubscriberNumber { get; set; } = null!;
        public string NodeAddress { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public string NodeName { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public string Username { get; set; } = null!;

        public virtual Ticket SubscriberNumberNavigation { get; set; } = null!;
        public virtual ICollection<Diagnostic> Diagnostics { get; set; }
    }
}
