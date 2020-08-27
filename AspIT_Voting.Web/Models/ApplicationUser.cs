using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string FullName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]        
        public DateTime? GraduationDate { get; set; }
        
        public virtual ICollection<UserActivity> UserActivities { get; set; }
        public virtual ICollection<UserFood> UserFoods { get; set; }

        public static IEnumerable<ApplicationUser> GetGraduatedUser(IEnumerable<ApplicationUser> applicationUsers)
        {            
            applicationUsers = applicationUsers.Where(d => d.GraduationDate <= DateTime.Now);
            return applicationUsers;
        }

        public static IEnumerable<ApplicationUser> GetUnGraduatedUser(IEnumerable<ApplicationUser> applicationUsers)
        {
            applicationUsers = applicationUsers.Where(d => d.GraduationDate > DateTime.Now || d.GraduationDate == null);
            return applicationUsers;
        }
    }
}
