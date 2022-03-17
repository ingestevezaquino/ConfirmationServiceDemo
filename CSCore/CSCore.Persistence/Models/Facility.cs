using System;
using System.Collections.Generic;

namespace CSCore.Persistence.Models
{
    public partial class Facility
    {
        public int Id { get; set; }
        public string NodeAddress { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public string NodeName { get; set; } = null!;
        public DateTime? CreationTime { get; set; }
        public string? HostName { get; set; }
        public string? UserName { get; set; }
    }
}
