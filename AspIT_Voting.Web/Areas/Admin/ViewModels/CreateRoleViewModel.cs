using System.ComponentModel.DataAnnotations;

namespace AspIT_Voting.Web.Areas.Admin.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Feltet skal udfyldes")]
        [Display(Name = "Rolle navn")]
        public string RoleName { get; set; }
    }
}
