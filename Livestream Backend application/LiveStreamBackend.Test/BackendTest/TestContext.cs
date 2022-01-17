using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Livestream_Backend_application.Models;

namespace LiveStreamBackend.Test
{
    public class TestContext : IDbContextFactory<LivestreamDBContext>
    {
        public DbContextOptions<LivestreamDBContext> options { get; private set; }

        public TestContext()
        {
            options = new DbContextOptionsBuilder<LivestreamDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        public LivestreamDBContext CreateDbContext()
        {
            return new LivestreamDBContext(options);
        }
    }
}
