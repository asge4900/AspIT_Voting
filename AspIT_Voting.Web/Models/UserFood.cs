﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace AspIT_Voting.Web.Models
{
    public partial class UserFood
    {
        public string UserId { get; set; }
        public int FoodId { get; set; }

        public virtual Food Food { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}