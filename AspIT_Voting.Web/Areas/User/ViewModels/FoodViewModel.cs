using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIT_Voting.Web.Areas.User.ViewModels
{
    public class FoodViewModel
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int VoteCount { get; set; }

        public bool IsThumbsUp { get; set; }
    }
}
