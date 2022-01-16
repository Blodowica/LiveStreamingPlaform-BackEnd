using Livestream_Backend_application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Models
{
    public  class AppStreams
    {
        public int StreamId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
