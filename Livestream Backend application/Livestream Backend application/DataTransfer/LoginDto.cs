using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.DataTransfer
{
    public class LoginDto
    {
        //THe information the user needs to provide to login
        public string  Email{ get; set; }

        public string   Password{ get; set; }
    }
}
