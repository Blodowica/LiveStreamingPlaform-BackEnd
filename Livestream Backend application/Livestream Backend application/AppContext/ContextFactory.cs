using Livestream_Backend_application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.AppContext
{
    public class ContextFactory : IDesignTimeDbContextFactory<LivestreamDBContext>, IDbContextFactory
    {

        public ContextFactory()
        {
            string path = Directory.GetCurrentDirectory();

            IConfigurationBuilder builder =
                new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json");

            IConfigurationRoot config = builder.Build();

            connectionString = config.GetConnectionString("LivestreamDataBase");
        }

        private readonly string connectionString;
        public ContextFactory(string connectionString)
        {
            this.connectionString = connectionString;

            var options = new DbContextOptionsBuilder<LivestreamDBContext>();
            options.UseSqlServer(connectionString);
        }

        public LivestreamDBContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<LivestreamDBContext>();
            options.UseSqlServer(connectionString);

            return new LivestreamDBContext(options.Options);
        }
    }
}
