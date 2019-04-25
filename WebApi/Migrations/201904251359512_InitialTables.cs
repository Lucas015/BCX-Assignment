namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyTasks",
                c => new
                    {
                        DailyTaskId = c.Int(nullable: false, identity: true),
                        TaskRef = c.Int(nullable: false),
                        TaskTime = c.Int(nullable: false),
                        EmployeeRef = c.Int(nullable: false),
                        HourlyRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AssignedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DailyTaskId)
                .ForeignKey("dbo.Employees", t => t.EmployeeRef, cascadeDelete: true)
                .ForeignKey("dbo.TaskDetails", t => t.TaskRef, cascadeDelete: true)
                .Index(t => t.TaskRef)
                .Index(t => t.EmployeeRef);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmployeeRoleRef = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.EmployeeRoles", t => t.EmployeeRoleRef, cascadeDelete: true)
                .Index(t => t.EmployeeRoleRef);
            
            CreateTable(
                "dbo.EmployeeRoles",
                c => new
                    {
                        RoldeId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RoldeId);
            
            CreateTable(
                "dbo.TaskDetails",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false),
                        TaskDescription = c.String(),
                        TaskDuration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailyTasks", "TaskRef", "dbo.TaskDetails");
            DropForeignKey("dbo.DailyTasks", "EmployeeRef", "dbo.Employees");
            DropForeignKey("dbo.Employees", "EmployeeRoleRef", "dbo.EmployeeRoles");
            DropIndex("dbo.Employees", new[] { "EmployeeRoleRef" });
            DropIndex("dbo.DailyTasks", new[] { "EmployeeRef" });
            DropIndex("dbo.DailyTasks", new[] { "TaskRef" });
            DropTable("dbo.TaskDetails");
            DropTable("dbo.EmployeeRoles");
            DropTable("dbo.Employees");
            DropTable("dbo.DailyTasks");
        }
    }
}
