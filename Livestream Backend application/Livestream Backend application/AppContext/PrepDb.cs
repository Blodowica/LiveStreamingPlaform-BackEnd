using Livestream_Backend_application.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.AppContext
{
    public class PrepDb
    {
            public static void PrepMigration(IApplicationBuilder app)
            {
                using var serviceScope = app.ApplicationServices.CreateScope();
                ApplyMigrations(serviceScope.ServiceProvider.GetService<LivestreamDBContext>());
            }

            private static void ApplyMigrations(LivestreamDBContext contextFactory)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    using var context = contextFactory;

                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }
        
    }
}

