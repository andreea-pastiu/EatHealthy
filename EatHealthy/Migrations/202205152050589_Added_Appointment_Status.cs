namespace EatHealthy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Appointment_Status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "Status");
        }
    }
}
