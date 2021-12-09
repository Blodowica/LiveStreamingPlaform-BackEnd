using Livestream_Backend_application.DataTransfer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
           _config = config;
        }

        public string CreateToken(AppUser user)
        {

            //user claims 

            var claims = new List<Claim>
          {
            new Claim(ClaimTypes.Name,  user.FirstName),
            new Claim(ClaimTypes.NameIdentifier,  user.Id),
            new Claim(ClaimTypes.Email,  user.Email),

          };


            //creating a key, credntials and descriptor for the tokens


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7), //later refresh tokens 
                SigningCredentials = creds
            };

            //Creating the Token 

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


        }
    }
}
