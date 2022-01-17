using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.DataTransfer
{
    public class AppException
    {

        public AppException(int statusCode, string message, string details)
        {
            SatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int   SatusCode{ get; set; }
        public string  Message{ get; set; }

        public string Details  { get; set; }
    }
}
