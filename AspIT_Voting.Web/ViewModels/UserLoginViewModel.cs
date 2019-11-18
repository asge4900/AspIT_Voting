using System.ComponentModel.DataAnnotations;

namespace AspIT_Voting.Web.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        public string UserName { get; set; }
    }
}
