using Livestream_Backend_application.DataTransfer;
using Livestream_Backend_application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Controller
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<AppUser> userManger, SignInManager<AppUser> signInManager, TokenService tokenService)
        {
            _userManger = userManger;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }


        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManger.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false); // the user, user password, should user be locked out 

            if (result.Succeeded)
            {
                return CreateUserObject(user); 
            }
            return Unauthorized();
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManger.Users.AnyAsync(x => x.Email == registerDto.Email)) return BadRequest("Email Taken");
            if (await _userManger.Users.AnyAsync(x => x.UserName == registerDto.UserName)) return BadRequest("UserName Taken");
           // if (await _userManger.Users.AnyAsync(x => x.StreamKey == registerDto.StreamKey)) return BadRequest("Error Creating Streamkey");

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
            return BadRequest("Problem registering user");


        }
        [Authorize]
        [HttpGet]

        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {                                             //User  Get the user that is associated with the action 
            var user = await _userManger.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));


            return CreateUserObject(user);
        }

        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.Lastname,
                Email= user.Email,
                Image = null,
                Token = _tokenService.CreateToken(user)
            };
        }

    }
}
