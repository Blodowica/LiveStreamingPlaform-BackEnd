using Livestream_Backend_application.DataTransfer;
using Livestream_Backend_application.Models;
using Livestream_Backend_application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Extensions
{

        // CLASS USED TO NO CROWD THE STRATUP CLASS
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
           {
               opt.Password.RequireNonAlphanumeric = false;
               opt.Password.RequireUppercase = false;
              opt.Password.RequireDigit = false;
               opt.Password.RequireLowercase = false;

           })
                .AddEntityFrameworkStores<LivestreamDBContext>()
                .AddSignInManager<SignInManager<AppUser>>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    //how do we want the app to validate if the token is valid 
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false, //soo to be edited

                    };

                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chat")))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
                
            //add TokenService
            services.AddScoped<TokenService>();
            services.AddSignalR();
            return services;
          
        }
    }
}
