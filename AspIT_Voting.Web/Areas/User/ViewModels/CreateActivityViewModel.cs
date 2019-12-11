using AspIT_Voting.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspIT_Voting.Web.Areas.User.ViewModels
{
    public class CreateActivityViewModel
    {

        [Required(ErrorMessage = "Feltet skal udfyldes")]
        [Display(Name = "Aktvitet", Prompt = "Skriv en aktivitet eller vælg fra listen")]
        public string ActivityName { get; set; }

        public List<SelectListItem> ActivitySuggestionsList { get; set; }

    }
}
