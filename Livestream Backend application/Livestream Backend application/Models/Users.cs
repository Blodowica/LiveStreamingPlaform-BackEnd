using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Livestream_Backend_application.Models
{
    public partial class Users
    {
        public int UsersId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Streamkey { get; set; }
        public string Role { get; set; }
        public string Email { get;  set; }
        //        public  Streams Streams { get; set; }

    }
}
