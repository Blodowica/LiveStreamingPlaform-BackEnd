/*using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Livestream_Backend_application.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace LiveStreamBackend.Test
{
    public class TestContext : IDesignTimeDbContextFactory<LivestreamDBContext>
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

        public LivestreamDBContext CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }


    public class TestContextFactory : IDesignTimeDbContextFactory<LivestreamDBContext>
    {
        public DbContextOptions<LivestreamDBContext> options { get; private set; }

        public TestContextFactory()
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
*/