using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspIT_Voting.Web.ViewModels
{
    public class AdminRegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekræft password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not math.")]
        public string ConfirmPassword { get; set; }
    }
}
