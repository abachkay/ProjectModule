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
        public ActionResult Index()
        {
            using (var db = new ProjectModuleDBEntities())
            {
                var html = "<a href='google.com'></a>";
                var css = "";
                ViewBag.V1 = new TaskVerifier(db.Task.FirstOrDefault(), html, css).Verify().ToString();
                ViewBag.V2 = new TaskVerifier(db.Task.ToList()[1], html, css).Verify().ToString();
            }
            return View();
        }        
    }
}