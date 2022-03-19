namespace EatHealthy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Patient_ID = c.Int(),
                        Nutritionist_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patients", t => t.Patient_ID)
                .ForeignKey("dbo.Nutritionists", t => t.Nutritionist_ID)
                .Index(t => t.Patient_ID)
                .Index(t => t.Nutritionist_ID);
            
            CreateTable(
                "dbo.MealPlans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Patient_ID = c.Int(),
                        Nutritionist_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patients", t => t.Patient_ID)
                .ForeignKey("dbo.Nutritionists", t => t.Nutritionist_ID)
                .Index(t => t.Patient_ID)
                .Index(t => t.Nutritionist_ID);
            
            CreateTable(
                "dbo.MealPlanDays",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        Week = c.Int(nullable: false),
                        MealPlan_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MealPlans", t => t.MealPlan_ID)
                .Index(t => t.MealPlan_ID);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MealPlanDay_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MealPlanDays", t => t.MealPlanDay_ID)
                .Index(t => t.MealPlanDay_ID);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        Meal_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Meals", t => t.Meal_ID)
                .Index(t => t.Meal_ID);
            
            CreateTable(
                "dbo.FoodDiaries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Diary = c.String(),
                        Patient_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patients", t => t.Patient_ID)
                .Index(t => t.Patient_ID);
            
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Nutritionists",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Nutritionist_ID = c.Int(),
                        Weight = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                        BMI = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.ID)
                .ForeignKey("dbo.Nutritionists", t => t.Nutritionist_ID)
                .Index(t => t.ID)
                .Index(t => t.Nutritionist_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "Nutritionist_ID", "dbo.Nutritionists");
            DropForeignKey("dbo.Patients", "ID", "dbo.Users");
            DropForeignKey("dbo.Nutritionists", "ID", "dbo.Users");
            DropForeignKey("dbo.Administrators", "ID", "dbo.Users");
            DropForeignKey("dbo.Appointments", "Nutritionist_ID", "dbo.Nutritionists");
            DropForeignKey("dbo.MealPlans", "Nutritionist_ID", "dbo.Nutritionists");
            DropForeignKey("dbo.MealPlans", "Patient_ID", "dbo.Patients");
            DropForeignKey("dbo.FoodDiaries", "Patient_ID", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "Patient_ID", "dbo.Patients");
            DropForeignKey("dbo.MealPlanDays", "MealPlan_ID", "dbo.MealPlans");
            DropForeignKey("dbo.Meals", "MealPlanDay_ID", "dbo.MealPlanDays");
            DropForeignKey("dbo.Ingredients", "Meal_ID", "dbo.Meals");
            DropIndex("dbo.Patients", new[] { "Nutritionist_ID" });
            DropIndex("dbo.Patients", new[] { "ID" });
            DropIndex("dbo.Nutritionists", new[] { "ID" });
            DropIndex("dbo.Administrators", new[] { "ID" });
            DropIndex("dbo.FoodDiaries", new[] { "Patient_ID" });
            DropIndex("dbo.Ingredients", new[] { "Meal_ID" });
            DropIndex("dbo.Meals", new[] { "MealPlanDay_ID" });
            DropIndex("dbo.MealPlanDays", new[] { "MealPlan_ID" });
            DropIndex("dbo.MealPlans", new[] { "Nutritionist_ID" });
            DropIndex("dbo.MealPlans", new[] { "Patient_ID" });
            DropIndex("dbo.Appointments", new[] { "Nutritionist_ID" });
            DropIndex("dbo.Appointments", new[] { "Patient_ID" });
            DropTable("dbo.Patients");
            DropTable("dbo.Nutritionists");
            DropTable("dbo.Administrators");
            DropTable("dbo.FoodDiaries");
            DropTable("dbo.Ingredients");
            DropTable("dbo.Meals");
            DropTable("dbo.MealPlanDays");
            DropTable("dbo.MealPlans");
            DropTable("dbo.Appointments");
            DropTable("dbo.Users");
        }
    }
}
