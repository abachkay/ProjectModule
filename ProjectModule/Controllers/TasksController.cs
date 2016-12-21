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
using ProjectModule;

namespace ProjectModule.Controllers
{
    public class TasksController : ApiController
    {
        private ProjectModuleDBEntities db = new ProjectModuleDBEntities();

        // GET: api/Tasks
        [HttpGet]
        [Route("api/tasks")]
        public IHttpActionResult GetTask()
        {
            return Json(db.Task);
        }

        // GET: api/Tasks/5
        [HttpGet]
        [Route("api/tasks/{id}")]
        public IHttpActionResult GetTask(long id)
        {
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            return Json(task);
        }

        //        // PUT: api/Tasks/5
        //        public IHttpActionResult PutTask(long id, Task task)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }
        //
        //            if (id != task.Id)
        //            {
        //                return BadRequest();
        //            }
        //
        //            db.Entry(task).State = EntityState.Modified;
        //
        //            try
        //            {
        //                db.SaveChanges();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!TaskExists(id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //
        //            return StatusCode(HttpStatusCode.NoContent);
        //        }

        // POST: api/tasks/set
        [HttpPut]
        [Route("api/tasks")]
        public IHttpActionResult SetTask(Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var old = db.Task.Find(task.Id);
            if (old == null)
            {
                var newTask = (db.Task.Add(task));
                db.SaveChanges();
                return Json(newTask);
            }

            db.Entry(old).CurrentValues.SetValues(task);

            db.SaveChanges();

            return Json(db.Task.Find(task.Id));
        }

        [HttpDelete]
        [Route("api/tasks/{id}")]
        public IHttpActionResult DeleteTask(long id)
        {
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            db.Task.Remove(task);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(long id)
        {
            return db.Task.Count(e => e.Id == id) > 0;
        }
    }
}