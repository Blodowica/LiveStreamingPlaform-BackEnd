using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Livestream_Backend_application.Models
{
    public partial class Followers
    {
        public int FollowersId { get; set; }
        public int UserdId { get; set; }
        public string FollowerId { get; set; }
    }
}
