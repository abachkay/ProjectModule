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
                        result = CheckXPath(rule);
                        break;
                    case Rule.RuleType.XPathCss:
                        result = CheckXPathCss(rule);
                        break;
                }
                if (!result)
                    return false;
            }
            return true;
            var c = _doc.QuerySelector(".content");


            var stylesheet = new Parser().Parse(_cssCode);
            var color = stylesheet.StyleRules
                .Where(r => r.Value.Equals(".b"))
                .SelectMany(r => r.Declarations)
                .FirstOrDefault(d => d.Name.Equals("color", StringComparison.InvariantCultureIgnoreCase))?
                .Term
                .ToString();

            return true;
        }

        public bool CheckXPath(Rule rule)
        {
            return true;
        }

        public bool CheckXPathCss(Rule rule)
        {
            return true;
        }
    }
}