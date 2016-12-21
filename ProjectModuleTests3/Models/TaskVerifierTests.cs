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
        public void VerifyXPathPresentTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void VerifyElementStyleTest()
        {
            Assert.Fail();
        }
    }
}