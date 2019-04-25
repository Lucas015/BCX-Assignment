using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DailyTasksController : ApiController
    {
        private WebApiContext db = new WebApiContext();

        // GET: api/DailyTasks
        public IQueryable<DailyTask> GetDailyTasks()
        {
            return db.DailyTasks.Include(x => x.Employee).Include(x => x.TaskDetail);
        }

        // GET: api/DailyTasks/5
        [ResponseType(typeof(DailyTask))]
        public IHttpActionResult GetDailyTask(int id)
        {
            DailyTask dailyTask = db.DailyTasks.Include(x => x.Employee).Include(x => x.TaskDetail).Where(x => x.DailyTaskId == id).FirstOrDefault();
            if (dailyTask == null)
            {
                return NotFound();
            }

            return Ok(dailyTask);
        }

        // PUT: api/DailyTasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDailyTask(int id, DailyTask dailyTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dailyTask.DailyTaskId)
            {
                return BadRequest();
            }

            db.Entry(dailyTask).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DailyTasks
        [ResponseType(typeof(DailyTask))]
        public IHttpActionResult PostDailyTask(DailyTask dailyTask)
        {
            var employeeRole = db.Employees.Where(x => x.EmployeeId == dailyTask.EmployeeRef).Select(x => x.EmployeeRoleRef).FirstOrDefault();

            var employeeRate = db.EmployeeRoles.Where(x => x.RoldeId == employeeRole).Select(x => x.Rate).FirstOrDefault();
            var allocatedTime = db.TaskDetails.Where(x => x.TaskId == dailyTask.TaskRef).Select(x => x.TaskDuration).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dailyTask.HourlyRate = employeeRate;
            dailyTask.TaskTime = allocatedTime;

            db.DailyTasks.Add(dailyTask);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dailyTask.DailyTaskId }, dailyTask);
        }

        // DELETE: api/DailyTasks/5
        [ResponseType(typeof(DailyTask))]
        public IHttpActionResult DeleteDailyTask(int id)
        {
            DailyTask dailyTask = db.DailyTasks.Include(x => x.Employee).Include(x => x.TaskDetail).Where(x => x.DailyTaskId == id).FirstOrDefault();
            if (dailyTask == null)
            {
                return NotFound();
            }

            db.DailyTasks.Remove(dailyTask);
            db.SaveChanges();

            return Ok(dailyTask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DailyTaskExists(int id)
        {
            return db.DailyTasks.Count(e => e.DailyTaskId == id) > 0;
        }
    }
}