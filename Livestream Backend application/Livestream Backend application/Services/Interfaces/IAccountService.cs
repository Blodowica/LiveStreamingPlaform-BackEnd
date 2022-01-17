using Livestream_Backend_application.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Services.Interfaces
{
   public interface IAccountService
    {
        Task<ActionResult<UserDto>> Register(RegisterDto registerDto);
    }
}
