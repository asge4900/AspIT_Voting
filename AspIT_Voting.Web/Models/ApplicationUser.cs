using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIT_Voting.Web.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            UserActivities = new HashSet<UserActivity>();
            UserFoods = new HashSet<UserFood>();
        }

        public virtual ICollection<UserActivity> UserActivities { get; set; }
        public virtual ICollection<UserFood> UserFoods { get; set; }
    }
}
