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
        public IHttpActionResult GetTask()
        {
            return Json(db.Task);
        }

        // GET: api/Tasks/5
        [HttpGet]
        public IHttpActionResult GetTask(long id)
        {
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            return Json(task);
        }

        // POST: api/tasks/set
        [HttpPost]
        [Route("api/tasks/set")]
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

        [HttpGet]
        [Route("api/tasks/delete/{id}")]
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