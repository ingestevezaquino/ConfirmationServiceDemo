using System;
using System.Collections.Generic;

namespace CSDiagADSL.Persistence.Models
{
    public partial class Diagnostic
    {
        public int Id { get; set; }
        public int FacilityId { get; set; }
        public bool IsConfigured { get; set; }
        public bool OLTAdminState { get; set; }
        public bool OLTOperState { get; set; }
        public bool ONTAdminState { get; set; }
        public bool ONTOperState { get; set; }
        public bool ONTRxPower { get; set; }
        public bool ONTTxPower { get; set; }
        public bool ONTVoltage { get; set; }
        public DateTime CreationTime { get; set; }
        public string Username { get; set; } = null!;

        public virtual Facility Facility { get; set; } = null!;
    }
}
