﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using AspIT_Voting.Web.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AspIT_Voting.Web.Data
{
    public partial class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivitySuggestion> ActivitySuggestions { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<FoodSuggestion> FoodSuggestions { get; set; }       
        public virtual DbSet<UserActivity> UserActivities { get; set; }
        public virtual DbSet<UserFood> UserFoods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.ActivityName).IsRequired();

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ActivitySuggestion>(entity =>
            {
                entity.Property(e => e.ActivitySuggestionName).IsRequired();
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FoodName).IsRequired();
            });

            modelBuilder.Entity<FoodSuggestion>(entity =>
            {
                entity.Property(e => e.FoodSuggestionName).IsRequired();
            });           

            modelBuilder.Entity<UserActivity>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ActivityId });

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.UserActivities)
                    .HasForeignKey(d => d.ActivityId)
                    .HasConstraintName("FK__UserActiv__Activ__2E1BDC42");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.UserActivities)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserActiv__UserI__2D27B809");
            });

            modelBuilder.Entity<UserFood>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.FoodId });

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.UserFoods)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK__UserFoods__FoodI__31EC6D26");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.UserFoods)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserFoods__UserI__30F848ED");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}