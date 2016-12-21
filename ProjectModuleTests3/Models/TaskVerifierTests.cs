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
            var html = "<a href='google.com'></a>";
            var css = "";
            Task task = new Task()
            {
                Id = 1,
                Description = "Bla-blah",
                Name = "sgfh",
                Rule = new List<Rule>()
                {
                    new Rule()
                    {
                        Id = 1, Selector = "//a/@href", Task = null,
                        TaskId = 1, Type = (long)TaskType.XPathQuery,
                        Value = "'google.com'"
                    }
                }
            };
            Assert.IsTrue(new TaskVerifier(task, html, css).Verify());
        }

        [TestMethod()]
        public void VerifyXPathPresentTest()
        {
            var html = "<html><a></a></html>";
            var css = "";
            Task task = new Task()
            {
                Id = 1,
                Description = "Bla-blah",
                Name = "sgfh",
                Rule = new List<Rule>()
                {
                    new Rule()
                    {
                        Id = 1, Selector = "//a", Task = null,
                        TaskId = 1, Type = (long)TaskType.XPathPresent,
                    }
                }
            };
            Assert.IsTrue(new TaskVerifier(task, html, css).Verify());
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
                        Id = 1, Selector = "//div[@id='a1']", Task = null,
                        TaskId = 1, Type = (long)TaskType.XPathElementStyle,
                        Value = "color:red"
                    }
                }
            };
            Assert.IsTrue(new TaskVerifier(task, html, css).Verify());
        }
    }
}