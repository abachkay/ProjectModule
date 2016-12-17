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
                var tasks = db.Tasks.ToList();
                ViewBag.Tasks = tasks;
            }
            return View();
        }        
    }
}