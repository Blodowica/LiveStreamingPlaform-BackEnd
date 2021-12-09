using Livestream_Backend_application.DataTransfer;
using Livestream_Backend_application.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Seed
{
    // IF DATABASE IS EMPTY SEED THE FOLLOWING DATA INTO THE DATABSE    
    public class Seed
    {

        public static async Task SeedData(LivestreamDBContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
              
                var users = new List<AppUser>
                {
                    new AppUser{UserName = "Bob12", FirstName= "Bob", Email = "bob@test.com" ,Lastname="The builder" , StreamKey="bob?key=bobsecretkey", Role="Admin"},
                    new AppUser{UserName  = "Tom21", FirstName = "tom", Email = "tom@test.com" ,Lastname="Holland" , StreamKey="tom?key=tomsecretkey", Role="User"},
                    new AppUser{UserName  = "Jane22", FirstName = "jane", Email = "jane@test.com", Lastname="Doe" , StreamKey="jane?key=janesecretkey", Role="User"},
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

           
        }
    }
}
