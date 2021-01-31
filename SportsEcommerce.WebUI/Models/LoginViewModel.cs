using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsEcommerce.WebUI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UsernName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}