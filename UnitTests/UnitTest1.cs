using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectModule.Models;
using ProjectModule.Models.Testing;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ElementStyle()
        {
            string htmlCode = @"
                <html>
                    <head></head>
                    <body>
                        <div class='content'>Anchor</div>
                    </body>
                </html>";
            string cssCode = @".content{color:red; background-color:white} .b{color:green}";
            var task = new Task()
            {
                Text = "Bla-blah",
                Rules = new List<Rule>()
                {
                    new Rule()
                    {
                        Type = Rule.RuleType.XPathCss,
                        XPathQuery = "//div",
                        CssProperties = new List<Rule.CssProperty>()
                        {
                            new Rule.CssProperty() {Name = "color", Value = "red"},
                            new Rule.CssProperty() {Name = "background-color", Value = "white"}
                        }
                    }
                }
            };
            bool passed = new TaskVerifier(task, htmlCode, cssCode).Check();
            Assert.IsTrue(passed);
        }
    }
}
