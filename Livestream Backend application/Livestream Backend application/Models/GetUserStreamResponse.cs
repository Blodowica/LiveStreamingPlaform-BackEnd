using Livestream_Backend_application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Models
{
    public class GetUserStreamResponse
    {
        public int StreamId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string FirstName  { get; set; }

    }
}
