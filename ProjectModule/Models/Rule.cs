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
                XPathElementPresent,
                XPathQuery,
                XPathElementStyle,
                XPathElementAttributes
            }

            public RuleType Type { get; set; }
            public string Selector { get; set; }
            public string Value { get; set; }
        }
    }
}