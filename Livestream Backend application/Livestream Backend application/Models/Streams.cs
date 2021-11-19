using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Models
{
    public class Streams
    {
        public int stream_id { get; set; }
        public int user_id { get; set; }

        public string title { get; set; }

        public  string description { get; set; }

    }
}
