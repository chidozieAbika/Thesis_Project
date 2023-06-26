using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Motte_IT.Models
{
    public class clientuser
    {
        public int ClientID { get; set; }
        
        [Required(ErrorMessage = "Please enter correct username")]
        [Display(Name = "Enter username :")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Please enter correct Password")]
        [Display(Name = "Enter Password :")]
        [DataType(DataType.Password)]
        public String PasswordHash { get; set; }
        public String Email { get; set; }


    }

}
