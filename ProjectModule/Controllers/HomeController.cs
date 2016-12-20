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
            //using (var db = new ProjectModuleDBEntities())
            //{
            //    db.Task.Add(new Task() { Name="b" , Description="bla"});
            //    db.SaveChanges();
            //    db.Rule.Add(new Rule() { Type = 1, Selector = "//a/@id", Value = "google.com", TaskId = 1 });
            //    db.SaveChanges();
            //}
            return View();
        }        
    }
}