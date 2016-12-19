using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectModule.Models
{
    namespace Testing
    {
        public class Rule
        {
            public enum RuleType
            {
                XPath,
                XPathCss
            }
            public RuleType Type { get; set; }
            public string XPathQuery { get; set; }
            public string XPathResult { get; set; }
            public string CssSelector { get; set; }
            public string CssAttribute { get; set; }
            public string CssValue { get; set; }
        }
    }
}