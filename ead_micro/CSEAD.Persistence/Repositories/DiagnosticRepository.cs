using CSEAD.Persistence.Interfaces;
using CSEAD.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEAD.Persistence.Repositories
{
    public class DiagnosticRepository : Repository<Diagnostic>, IDiagnosticRepository
    {
        public DiagnosticRepository(ApplicationDBContext context)
            : base(context)
        {
        }

        public ApplicationDBContext Context
        {
            get { return Context as ApplicationDBContext; }
        }
    }
}
