﻿using System;
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
                Description = "Bla-blah",
                Rules = new List<Rule>()
                {
                    new Rule()
                    {
                        Type = Rule.RuleType.XPathElementStyle,
                        Selector = "//div",
                        Value = "color:red; background-color:white"
                    }
                }
            };
            bool passed = new TaskVerifier(task, htmlCode, cssCode).Verify();
            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void ElementAttributes()
        {
            string htmlCode = @"
                <html>
                    <head></head>
                    <body>
                        <div class='content'>D1</div>
                        <div title='t' class='content'>D2</div>
                    </body>
                </html>";
            string cssCode = @".content{color:red; background-color:white} .b{color:green}";
            var task = new Task()
            {
                Description = "Bla-blah",
                Rules = new List<Rule>()
                {
                    new Rule()
                    {
                        Type = Rule.RuleType.XPathElementAttributes,
                        Selector = "//div",
                        Value = "class='content' title='t'"
                    }
                }
            };
            bool passed = new TaskVerifier(task, htmlCode, cssCode).Verify();
            Assert.IsTrue(passed);
        }
    }
}
