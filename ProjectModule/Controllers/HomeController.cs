using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectModule.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using ExCSS;

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

            string htmlCode = @"
                <html>
                    <head></head>
                    <body>
                        <div class='content'>Anchor</div>
                    </body>
                </html>";
            var html = new HtmlDocument();
            html.LoadHtml(htmlCode);
            var doc = html.DocumentNode;
            var c = doc.QuerySelector(".content");

            string cssCode = @".content{color:red} .b{color:green}";

            var stylesheet = new Parser().Parse(cssCode);
            var color = stylesheet.StyleRules
                .Where(r => r.Value.Equals(".b"))
                .SelectMany(r => r.Declarations)
                .FirstOrDefault(d => d.Name.Equals("color", StringComparison.InvariantCultureIgnoreCase))?
                .Term
                .ToString();

            return View();
        }        
    }
}