using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectModule.Models.Testing;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using ExCSS;

namespace ProjectModule.Models
{
    public class TaskVerifier
    {
        private Task _task;
        private string _htmlCode;
        private string _cssCode;
        private HtmlDocument _html;
        private HtmlNode _doc;
        private StyleSheet _styleSheet;

        public TaskVerifier(Task task, string htmlCode, string cssCode)
        {
            _task = task;
            _htmlCode = htmlCode;
            _cssCode = cssCode;
            _html = new HtmlDocument();
            _html.LoadHtml(htmlCode);
            _doc = _html.DocumentNode;

            _styleSheet = new Parser().Parse(_cssCode);
        }

        public bool Check()
        {
            foreach (var rule in _task.Rules)
            {
                bool result = true;
                switch (rule.Type)
                {
                    case Rule.RuleType.XPath:
                        result = CheckXPathResult(rule);
                        break;
                    case Rule.RuleType.XPathCss:
                        result = CheckElementStyle(rule);
                        break;
                }
                if (!result)
                    return false;
            }
            return true;
        }

        public bool CheckXPathResult(Rule rule)
        {
            return true;
        }

        public bool CheckXPathPresent(Rule rule)
        {
            return true;
        }

        public bool CheckElementStyle(Rule rule)
        {
            var nodeByXPath = _doc.SelectSingleNode(rule.XPathQuery);

            foreach (var styleRule in _styleSheet.StyleRules)
            {
                string selector = styleRule.Value;
                var nodeByCss = _doc.QuerySelector(selector);
                if (nodeByCss == nodeByXPath)
                {
                    bool verified = true;
                    foreach (var sampleProperty in rule.CssProperties)
                    {
                        var property = styleRule.Declarations
                            .FirstOrDefault(d => d.Name.Equals(sampleProperty.Name));
                        if (property == null || !property.Term.ToString()
                            .Equals(sampleProperty.Value, StringComparison.InvariantCultureIgnoreCase))
                        {
                            verified = false;
                            break;
                        }
                    }
                    if (verified) return true;
                }
            }
            return false;
        }
    }
}