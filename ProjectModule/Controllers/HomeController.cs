using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectModule.Models;

namespace ProjectModule.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    using (var db = new ProjectModuleDBEntities())
        //    {
        //        //var html = "<a href='google.com'></a>";
        //        //var css = "";
        //        //ViewBag.V1 = new TaskVerifier(db.Task.FirstOrDefault(), html, css).Verify().ToString();
        //        //ViewBag.V2 = new TaskVerifier(db.Task.ToList()[1], html, css).Verify().ToString();
        //        ViewBag.Tasks = db.Task.ToList();
        //        ViewBag.CurrentTask = db.Task.FirstOrDefault();
        //    }
        //    return View();
        //}
        [HttpGet]
        public ActionResult Index(int taskId=0)
        {
            using (var db = new ProjectModuleDBEntities())
            {                
                ViewBag.Tasks = db.Task.ToList();
                var task=db.Task.ToList().Where(x=>x.Id==taskId).FirstOrDefault();
                ViewBag.CurrentTask = (task == null ?db.Task.FirstOrDefault():task);
                if (TempData["TaskResult"]!=null)
                    if(TempData["TaskResult"].ToString()=="Correct")
                        ViewBag.TaskResult = "Correct";
                    else
                        ViewBag.TaskResult = "Incorrect";
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Verify(int taskId,string htmlCode, string cssCode)
        {
            using (var db = new ProjectModuleDBEntities())
            {
                var task = db.Task.Where(x => x.Id == taskId).FirstOrDefault();
                if (task != null)
                {
                    if (new TaskVerifier(task, htmlCode, cssCode).Verify())
                        TempData["TaskResult"] = "Correct";
                    else
                        TempData["TaskResult"] = "Incorrect";
                }
            }                            
            return RedirectToAction("Index", new { taskId=taskId});
        }
    }
}