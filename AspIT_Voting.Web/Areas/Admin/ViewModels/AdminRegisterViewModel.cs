using System.ComponentModel.DataAnnotations;

namespace AspIT_Voting.Web.Areas.Admin.ViewModels
{
    public class AdminRegisterViewModel
    {
        [Required (ErrorMessage = "Angiv brugernavn")]
        [Display(Name = "Brugernavn")]
        public string UserName { get; set; }

        [Required (ErrorMessage = "Angiv adgangskode")]
        [DataType(DataType.Password)]
        [Display(Name = "Adgangskode")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekræft adgangskode")]
        [Compare("Password", ErrorMessage = "Adgangskode and bekræft adgangskode er ikke ens.")]
        public string ConfirmPassword { get; set; }
    }
}
