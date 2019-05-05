using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Loners.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "User name is required.")]
        [DisplayName("User name")]
        public string UserName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password name is required.")]
        [DisplayName("Password"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
