using CSEAD.Persistence.Interfaces;
using CSEAD.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEAD.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            Diagnostics = new DiagnosticRepository(_context);
            Facilities = new FacilityRepository(_context);
        }

        public IDiagnosticRepository Diagnostics { get; private set; }
        public IFacilityRepository Facilities { get; private set; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
