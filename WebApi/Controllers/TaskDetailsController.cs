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
    public class TaskDetailsController : ApiController
    {
        private WebApiContext db = new WebApiContext();

        // GET: api/TaskDetails
        public IQueryable<TaskDetail> GetTaskDetails()
        {
            return db.TaskDetails;
        }

        // GET: api/TaskDetails/5
        [ResponseType(typeof(TaskDetail))]
        public IHttpActionResult GetTaskDetail(int id)
        {
            TaskDetail taskDetail = db.TaskDetails.Find(id);
            if (taskDetail == null)
            {
                return NotFound();
            }

            return Ok(taskDetail);
        }

        // PUT: api/TaskDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTaskDetail(int id, TaskDetail taskDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskDetail.TaskId)
            {
                return BadRequest();
            }

            db.Entry(taskDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskDetailExists(id))
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

        // POST: api/TaskDetails
        [ResponseType(typeof(TaskDetail))]
        public IHttpActionResult PostTaskDetail(TaskDetail taskDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TaskDetails.Add(taskDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = taskDetail.TaskId }, taskDetail);
        }

        // DELETE: api/TaskDetails/5
        [ResponseType(typeof(TaskDetail))]
        public IHttpActionResult DeleteTaskDetail(int id)
        {
            TaskDetail taskDetail = db.TaskDetails.Find(id);
            if (taskDetail == null)
            {
                return NotFound();
            }

            db.TaskDetails.Remove(taskDetail);
            db.SaveChanges();

            return Ok(taskDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskDetailExists(int id)
        {
            return db.TaskDetails.Count(e => e.TaskId == id) > 0;
        }
    }
}