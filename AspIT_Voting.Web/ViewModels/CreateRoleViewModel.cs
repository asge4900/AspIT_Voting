using System.ComponentModel.DataAnnotations;

namespace AspIT_Voting.Web.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
