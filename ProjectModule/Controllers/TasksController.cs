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
        private readonly ProjectModuleDBEntities _db = new ProjectModuleDBEntities();

        [HttpGet]
        [Route("api/tasks")]
        public IHttpActionResult GetTask()
        {
            return Json(_db.Task); 
        }

        [HttpGet]
        [Route("api/tasks/{id}")]
        public IHttpActionResult GetTask(long id)
        {
            Task task = _db.Task.Find(id);
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

        [HttpPut]
        [Route("api/tasks")]
        public IHttpActionResult SetTask(Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var old = _db.Task.Find(task.Id);
            if (old == null)
            {
                var newTask = (_db.Task.Add(task));
                _db.SaveChanges();
                return Json(newTask);
            }

            _db.Entry(old).CurrentValues.SetValues(task);

            _db.SaveChanges();

            return Json(_db.Task.Find(task.Id));
        }

        [HttpDelete]
        [Route("api/tasks/{id}")]
        public IHttpActionResult DeleteTask(long id)
        {
            Task task = _db.Task.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _db.Task.Remove(task);
            _db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(long id)
        {
            return _db.Task.Count(e => e.Id == id) > 0;
        }
    }
}