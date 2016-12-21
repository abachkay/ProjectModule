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

        // POST: api/tasks
        [HttpPost]
        public IHttpActionResult SetTask(Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Entry(db.Task.Find(task.Id)).CurrentValues.SetValues(task);

            db.SaveChanges();

            return Json(db.Task.Find(task.Id));
        }

        // DELETE: api/Tasks/5
//        [ResponseType(typeof(Task))]
//        public IHttpActionResult DeleteTask(long id)
//        {
//            Task task = db.Task.Find(id);
//            if (task == null)
//            {
//                return NotFound();
//            }
//
//            db.Task.Remove(task);
//            db.SaveChanges();
//
//            return Ok(task);
//        }

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