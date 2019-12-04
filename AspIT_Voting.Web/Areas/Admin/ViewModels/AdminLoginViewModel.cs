using System.ComponentModel.DataAnnotations;

namespace AspIT_Voting.Web.Areas.Admin.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "Angiv brugernavn")]
        [Display(Name = "Brugernavn")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Angiv adgangskode")]
        [Display(Name = "Adgangskode")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
