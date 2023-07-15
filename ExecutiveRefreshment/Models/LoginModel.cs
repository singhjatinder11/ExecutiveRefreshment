using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExecutiveRefreshment.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "User Name")]
         
        public string user_name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        
    }

    public class ForgotPasswordModel
    {
        
        public string user_name { get; set; }

         
        public string email { get; set; }


    }
}