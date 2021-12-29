using Livestream_Backend_application.DataTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Livestream_Backend_application.Models
{
    public partial class Streams
    {
     
        public int StreamId { get; set; }
        public string Email  { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AppUser AppUser { get; set; }
    }
}
