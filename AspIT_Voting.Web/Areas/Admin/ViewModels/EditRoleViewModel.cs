using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspIT_Voting.Web.Areas.Admin.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "Rolle navn skal udfyldes")]
        [Display(Name = "Rolle navn")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
