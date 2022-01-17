using Livestream_Backend_application.DataTransfer;
using Livestream_Backend_application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;

        public AccountService(UserManager<AppUser> userManger, SignInManager<AppUser> signInManager, TokenService tokenService)
        {
            _userManger = userManger;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            
            var user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                StreamKey = registerDto.StreamKey,
                Lastname = registerDto.LastName
            };

            var result = await _userManger.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }
            return null;
        }

 

        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.Lastname,
                Email = user.Email,
                Image = null,
                Token = _tokenService.CreateToken(user),
                StreamKey = user.StreamKey

            };
        }

      
       
    }
}
