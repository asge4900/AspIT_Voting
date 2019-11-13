using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspIT_Voting.Web.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]        
        public string UserName { get; set; }
        

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
