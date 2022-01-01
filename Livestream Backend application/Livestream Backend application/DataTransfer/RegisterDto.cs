using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.DataTransfer
{
    public class RegisterDto
    {
        //informatoion the user needs to provide to register 
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password Must Have 6 Characters and contain atleast on Normal, Capital and Special Character")]
        public string   Password{ get; set; }
        public string  StreamKey  { get; set; }
    }
}
