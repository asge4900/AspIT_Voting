using AspIT_Voting.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspIT_Voting.Web.Areas.Admin.ViewModels
{
    public class AdminActivityViewModel
    {
        public AdminActivityViewModel()
        {
            Activities = new List<AdminActivityViewModel>();
            Users = new List<AdminActivityViewModel>();
        }

        public int ActivityId { get; set; }

        [Display(Name = "Aktivitet")]
        public string ActivityName { get; set; }

        [Display(Name = "Antal stemmer")]
        public int VoteCount { get; set; }

        [Display(Name = "Oprettelsesdato")]
        public DateTime CreationDate { get; set; }

        public string UserName { get; set; }

        public List<AdminActivityViewModel> Activities { get; set; }

        public List<AdminActivityViewModel> Users { get; set; }
    }
}
