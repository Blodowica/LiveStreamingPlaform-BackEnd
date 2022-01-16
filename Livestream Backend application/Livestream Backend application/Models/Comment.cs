using Livestream_Backend_application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string  Body{ get; set; }

        public AppUser Author { get; set; }

        public AppStreams  Stream { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
