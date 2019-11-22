using System.ComponentModel.DataAnnotations;

namespace AspIT_Voting.Web.Areas.Admin.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
