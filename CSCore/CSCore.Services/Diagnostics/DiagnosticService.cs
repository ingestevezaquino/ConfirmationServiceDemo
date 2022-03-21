using CSCore.Persistence;
using CSCore.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCore.Services.Diagnostics
{
    public class DiagnosticService : IDiagnosticService
    {
        private readonly ApplicationDBContext _context;

        public DiagnosticService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Diagnostic> GetLastDiagnosisAndFacility(string subcriberNumber)
        {
            var facility = await _context.Facilities
                .Include(x => x.Diagnostics).FirstOrDefaultAsync(x => x.SubscriberNumber == subcriberNumber);

            var diagnosis = facility.Diagnostics.OrderByDescending(d => d.CreationTime).First();
            diagnosis.Facility = facility;

            return diagnosis;
        }
    }
}
