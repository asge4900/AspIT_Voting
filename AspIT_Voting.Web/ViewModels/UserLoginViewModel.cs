using System.ComponentModel.DataAnnotations;

namespace AspIT_Voting.Web.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Angiv brugernavn")]
        [Display(Name = "Brugernavn")]
        public string UserName { get; set; }
    }
}
