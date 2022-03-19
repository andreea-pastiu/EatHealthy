using EatHealthy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EatHealthy
{
    public class EatHealthyContext : DbContext
    {
        public EatHealthyContext()
            : base("name=EatHealthy")
        {
        }
        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<FoodDiary> FoodDiaries { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<MealPlan> MealPlans { get; set; }
        public virtual DbSet<MealPlanDay> MealPlanDays { get; set; }
        public virtual DbSet<Nutritionist> Nutritionists { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>().ToTable("Administrators");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            modelBuilder.Entity<FoodDiary>().ToTable("FoodDiaries");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
            modelBuilder.Entity<Meal>().ToTable("Meals");
            modelBuilder.Entity<MealPlan>().ToTable("MealPlans");
            modelBuilder.Entity<MealPlanDay>().ToTable("MealPlanDays");
            modelBuilder.Entity<Nutritionist>().ToTable("Nutritionists");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}