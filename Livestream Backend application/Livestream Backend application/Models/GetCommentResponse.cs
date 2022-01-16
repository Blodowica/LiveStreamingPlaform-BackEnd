using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Models
{
    public class GetCommentResponse
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public string  Author { get; set; }

        public int StreamId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
