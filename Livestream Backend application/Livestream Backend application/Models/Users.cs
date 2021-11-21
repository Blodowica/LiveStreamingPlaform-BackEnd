using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Models
{
    public class Users
    {
        public int users_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string streamkey { get; set; }

        public string role { get; set; }



        public Users(int users_id ,string firstname , string lastname, string username, string email, string password, string streamkey, string role )
        {
            this.users_id = users_id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.username = username;
            this.email = email;
            this.password = password;
            this.streamkey = streamkey;
            this.role = role;
        }

    }
}
