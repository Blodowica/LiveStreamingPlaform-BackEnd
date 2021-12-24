using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Livestream_Backend_application.DataTransfer
{
    public class AppUser : IdentityUser
    {
        public string FirstName  { get; set; }

        public string  Lastname{ get; set; }

        public string   StreamKey{ get; set; }

        public string  Role { get; set; }

    }
}
