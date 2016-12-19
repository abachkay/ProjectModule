using Microsoft.VisualStudio.TestTools.UnitTesting;
using sqlLiteProbe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlLiteProbe.Tests
{
    [TestClass()]
    public class TaskVerifierTests
    {
        [TestMethod()]
        public void VerifyTest1()
        {
            string html = "<style>.ab{background-color:blue}</style><a class='ab' id='ab'></a><div id='div1'><a id='a2'></a></div>";
            string css = "#a2{color:red;}";
            var task = new Task()
            {
                Description = "bla",
                Rules = new Rule[] {
                    new Rule() { ElementXPath = "//a",
                    StyleProperties= new StyleProperty[]
                        {
                            new StyleProperty {Name="color",Value="red"},
                        },
                    },
                    new Rule() { ElementXPath = "//a[@class='ab']" },
                    new Rule()
                    {
                        ElementXPath = "//div[@id='div1']/a", StyleProperties= new StyleProperty[]
                        {
                            new StyleProperty {Name="color",Value="red"},
                        },
                    },
                    new Rule()
                    {
                        ElementXPath = "//a", Attributes= new Attribute[]
                        {
                            new Attribute() { Name="class", Value="ab"}
                        },
                        StyleProperties= new StyleProperty[]
                        {
                            new StyleProperty {Name="color",Value="red"},
                            new StyleProperty {Name="background-color",Value="blue"},
                        }
                    },
                }
            };
            var result = TaskVerifier.Verify(html, css, task);
            Assert.AreEqual(true,result);
        }
    }
}