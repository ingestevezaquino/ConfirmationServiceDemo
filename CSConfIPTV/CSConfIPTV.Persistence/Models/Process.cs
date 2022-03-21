using System;
using System.Collections.Generic;

namespace CSConfIPTV.Persistence.Models
{
    public partial class Process
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public string Username { get; set; } = null!;
    }
}
