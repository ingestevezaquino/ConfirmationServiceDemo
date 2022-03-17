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
    public abstract class DemoTicketsDBFixture
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly DemoTicketsDBContext DbContext;

        public DemoTicketsDBFixture()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<DemoTicketsDBContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new DemoTicketsDBContext(options);
            DbContext.Database.EnsureCreated();
        }

        public DemoTicketsDBContext GetInMemoryTicketsDBContext()
        {
            var _contextOptions = new DbContextOptionsBuilder<DemoTicketsDBContext>()
              .UseInMemoryDatabase("DemoTicketsDBContextTest")
              .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
              .Options;

            return new DemoTicketsDBContext(_contextOptions);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
