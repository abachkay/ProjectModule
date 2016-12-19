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

            public class CssProperty
            {
                public string Name { get; set; }
                public string Value { get; set; }
            }

            public RuleType Type { get; set; }
            public string XPathQuery { get; set; }
            public string XPathResult { get; set; }
            public string CssSelector { get; set; }
            public List<CssProperty> CssProperties { get; set; }
        }
    }
}