﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace AspIT_Voting.Web.Models
{
    public partial class Food
    {
        public Food()
        {
            UserFoods = new HashSet<UserFood>();
        }

        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int VoteCount { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<UserFood> UserFoods { get; set; }
    }
}