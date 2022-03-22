using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEAD.API.DTOs
{
    public class DiagnosticDto
    {
        public bool IsConfigured { get; set; }
        public bool OLTAdminState { get; set; }
        public bool OLTOperState { get; set; }
        public bool ONTAdminState { get; set; }
        public bool ONTOperState { get; set; }
        public bool ONTRxPower { get; set; }
        public bool ONTTxPower { get; set; }
        public bool ONTVoltage { get; set; }

        public virtual FacilityDto Facility { get; set; } = null!;
    }
}
