using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModule.Models.Tests
{
    [TestClass()]
    public class TaskVerifierTests
    {       
        [TestMethod()]
        public void VerifyXPathQueryResultTest()
        {
            using (var db = new ProjectModuleDBEntities())
            {
                var html = "<a></a>";
                var css = "";
                Assert.IsTrue(new TaskVerifier(db.Task.FirstOrDefault(), html, css).Verify());
            }                
        }     

        [TestMethod()]
        public void VerifyElementStyleTest()
        {
            var html = 
                @"<html> 
                    <div class='a' id='a1'></div>
                    <div class='b'></div>
                </html>";
            var css = ".a{color:red}";
            Task task = new Task()
            {
                Id = 1,
                Description = "Bla-blah",
                Name = "sgfh",
                Rule = new List<Rule>()
                {
                    new Rule()
                    {
                        Id = 1, Selector = "//div[@id='a1']",
                        TaskId = 1, Type = (long)TaskType.XPathElementStyle,
                        Value = "color:red"
                    }
                }
            };
            Assert.IsTrue(new TaskVerifier(task, html, css).Verify());
        }
    }
}