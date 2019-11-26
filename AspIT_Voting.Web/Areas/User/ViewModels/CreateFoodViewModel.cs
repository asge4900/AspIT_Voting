using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspIT_Voting.Web.Areas.User.ViewModels
{
    public class CreateFoodViewModel
    {
        [Required]
        public string FoodName { get; set; }

        public List<SelectListItem> FoodSuggestionsList { get; set; }
    }
}
