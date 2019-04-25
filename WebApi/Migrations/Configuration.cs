namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApi.Models.WebApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApi.Models.WebApiContext context)
        {
            context.EmployeeRoles.AddOrUpdate(x => x.RoldeId,
                new EmployeeRole() { RoldeId = 1, RoleName = "Casual Employee Level 1", Rate = 1300 },
                new EmployeeRole() { RoldeId = 2, RoleName = "Casual Employee Level 2", Rate = 1600 });

            context.TaskDetails.AddOrUpdate(x => x.TaskId,
                new TaskDetail() { TaskId = 1, TaskName = "Authorize Debit orders", TaskDescription = "Playback all the conversation between debt collector and the  customer", TaskDuration = 4 },
                new TaskDetail() { TaskId = 2, TaskName = "Pack up the store room", TaskDescription = "Tidy and pack up sotre room on 5th floor", TaskDuration = 2 },
                new TaskDetail() { TaskId = 3, TaskName = "Data Capturing", TaskDescription = "Digitize all the stock revenue sheets into excell spreeadsheet", TaskDuration = 6 });

            context.Employees.AddOrUpdate(x => x.EmployeeId,
                new Employee() { EmployeeId = 1, FirstName = "Alonso", LastName = "Brown", EmployeeRoleRef = 1 },
                new Employee() { EmployeeId = 2, FirstName = "Jason", LastName = "Walker", EmployeeRoleRef = 2 },
                new Employee() { EmployeeId = 3, FirstName = "Alison", LastName = "Mid", EmployeeRoleRef = 1 },
                new Employee() { EmployeeId = 4, FirstName = "Audrey", LastName = "Lorenzos", EmployeeRoleRef = 2 },
                new Employee() { EmployeeId = 5, FirstName = "Johannes", LastName = "Kelly", EmployeeRoleRef = 1 },
                new Employee() { EmployeeId = 6, FirstName = "Oprah", LastName = "Devaughn", EmployeeRoleRef = 2 },
                new Employee() { EmployeeId = 7, FirstName = "Raheem", LastName = "August", EmployeeRoleRef = 2 },
                new Employee() { EmployeeId = 8, FirstName = "Alexadra", LastName = "Stevens", EmployeeRoleRef = 2 });

            context.DailyTasks.AddOrUpdate(x => x.DailyTaskId,
                new DailyTask() { DailyTaskId = 1, AssignedOn = DateTime.Now, TaskTime = 4, HourlyRate = 1600, EmployeeRef = 2, TaskRef = 1 },
                new DailyTask() { DailyTaskId = 2, AssignedOn = DateTime.Now, TaskTime = 6, HourlyRate = 1300, EmployeeRef = 1, TaskRef = 3 },
                new DailyTask() { DailyTaskId = 3, AssignedOn = DateTime.Now, TaskTime = 6, HourlyRate = 1300, EmployeeRef = 5, TaskRef = 3 },
                new DailyTask() { DailyTaskId = 4, AssignedOn = DateTime.Now, TaskTime = 2, HourlyRate = 1600, EmployeeRef = 3, TaskRef = 2 },
                new DailyTask() { DailyTaskId = 5, AssignedOn = DateTime.Now, TaskTime = 2, HourlyRate = 1600, EmployeeRef = 4, TaskRef = 2 },
                new DailyTask() { DailyTaskId = 6, AssignedOn = DateTime.Now, TaskTime = 4, HourlyRate = 1300, EmployeeRef = 5, TaskRef = 1 },
                new DailyTask() { DailyTaskId = 7, AssignedOn = DateTime.Now, TaskTime = 6, HourlyRate = 1600, EmployeeRef = 8, TaskRef = 3 },
                new DailyTask() { DailyTaskId = 8, AssignedOn = DateTime.Now, TaskTime = 4, HourlyRate = 1600, EmployeeRef = 8, TaskRef = 1 },
                new DailyTask() { DailyTaskId = 9, AssignedOn = DateTime.Now, TaskTime = 6, HourlyRate = 1300, EmployeeRef = 3, TaskRef = 3 },
                new DailyTask() { DailyTaskId = 10, AssignedOn = DateTime.Now, TaskTime = 2, HourlyRate = 1600, EmployeeRef = 9, TaskRef = 2 },
                new DailyTask() { DailyTaskId = 11, AssignedOn = DateTime.Now, TaskTime = 4, HourlyRate = 1600, EmployeeRef = 9, TaskRef = 1 },
                new DailyTask() { DailyTaskId = 12, AssignedOn = DateTime.Now, TaskTime = 6, HourlyRate = 1600, EmployeeRef = 2, TaskRef = 3 });
        }
    }
}
