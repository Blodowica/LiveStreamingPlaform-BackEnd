using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.DataTransfer
{

    //The information that needs to be send back when succesfully login 
    public class UserDto
    {
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string Token { get; set; }
    }
}
