using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIT_Voting.Web.Areas.User.ViewModels
{
    public class ActivityViewModel
    {
        public int ActivityId { get; set; }
        public string ActivtyName { get; set; }
        public int VoteCount { get; set; }

        public bool IsThumbsUp { get; set; }
    }
}
