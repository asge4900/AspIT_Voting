using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIT_Voting.Web.Areas.Admin.ViewModels
{
    public class AdminFoodViewModel
    {
        public AdminFoodViewModel()
        {
            Foods = new List<AdminFoodViewModel>();
            Users = new List<AdminFoodViewModel>();
        }

        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int VoteCount { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }

        public List<AdminFoodViewModel> Foods { get; set; }
        public List<AdminFoodViewModel> Users { get; set; }

    }
}
