using CSCore.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCore.Tests.Shared
{
    public abstract class ApplicationDBFixture
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly ApplicationDBContext DbContext;

        public ApplicationDBFixture()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new ApplicationDBContext(options);
            DbContext.Database.EnsureCreated();
        }

        public ApplicationDBContext GetInMemoryTicketsDBContext()
        {
            var _contextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
              .UseInMemoryDatabase("DemoTicketsDBContextTest")
              .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
              .Options;

            return new ApplicationDBContext(_contextOptions);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
