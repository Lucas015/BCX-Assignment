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
    public class EmployeeRolesController : ApiController
    {
        private WebApiContext db = new WebApiContext();

        // GET: api/EmployeeRoles
        public IQueryable<EmployeeRole> GetEmployeeRoles()
        {
            return db.EmployeeRoles;
        }

        // GET: api/EmployeeRoles/5
        [ResponseType(typeof(EmployeeRole))]
        public IHttpActionResult GetEmployeeRole(int id)
        {
            EmployeeRole employeeRole = db.EmployeeRoles.Find(id);
            if (employeeRole == null)
            {
                return NotFound();
            }

            return Ok(employeeRole);
        }

        // PUT: api/EmployeeRoles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeRole(int id, EmployeeRole employeeRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeRole.RoldeId)
            {
                return BadRequest();
            }

            db.Entry(employeeRole).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeRoleExists(id))
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

        // POST: api/EmployeeRoles
        [ResponseType(typeof(EmployeeRole))]
        public IHttpActionResult PostEmployeeRole(EmployeeRole employeeRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeRoles.Add(employeeRole);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeRole.RoldeId }, employeeRole);
        }

        // DELETE: api/EmployeeRoles/5
        [ResponseType(typeof(EmployeeRole))]
        public IHttpActionResult DeleteEmployeeRole(int id)
        {
            EmployeeRole employeeRole = db.EmployeeRoles.Find(id);
            if (employeeRole == null)
            {
                return NotFound();
            }

            db.EmployeeRoles.Remove(employeeRole);
            db.SaveChanges();

            return Ok(employeeRole);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeRoleExists(int id)
        {
            return db.EmployeeRoles.Count(e => e.RoldeId == id) > 0;
        }
    }
}