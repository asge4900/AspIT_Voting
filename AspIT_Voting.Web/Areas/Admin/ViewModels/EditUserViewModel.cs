using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspIT_Voting.Web.Areas.Admin.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {            
            Roles = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public IList<string> Roles { get; set; }
    }
}
