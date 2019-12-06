using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspIT_Voting.Web.Areas.Admin.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Angiv brugernavn")]
        [Display(Name = "Brugernavn")]
        public string UserName { get; set; }
    }
}
