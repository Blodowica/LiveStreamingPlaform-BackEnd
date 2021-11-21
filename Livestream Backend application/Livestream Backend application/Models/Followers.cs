using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Models
{
    public class Followers
    {
        public int followers_id { get; set; }
        public int user_id { get; set; }

        public int follower_id { get; set; }
    }
}
