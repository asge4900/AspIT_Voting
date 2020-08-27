using System;
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

        [Required(ErrorMessage = "Angiv brugernavn")]
        [Display(Name = "Brugernavn")]
        public string UserName { get; set; }

        [Display(Name = "Navn")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dimission")]
        public DateTime? GraduationDate { get; set; }

        public IList<string> Roles { get; set; }
    }
}
