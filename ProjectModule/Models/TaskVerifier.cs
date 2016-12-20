using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectModule.Models.Testing;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using ExCSS;
using System.IO;
using System.Xml.XPath;

namespace ProjectModule.Models
{
    public class TaskVerifier
    {
        private readonly Task _task;
        private readonly string _htmlCode;
        private readonly string _cssCode;

        private readonly HtmlDocument _html;
        private readonly HtmlDocument _htmlInlined;
        private readonly StyleSheet _styleSheet;

        public TaskVerifier(Task task, string htmlCode, string cssCode)
        {
            _task = task;
            _htmlCode = htmlCode;
            _cssCode = cssCode;
            _html = new HtmlDocument();
            _html.LoadHtml(htmlCode);

            _styleSheet = new Parser().Parse(_cssCode);

            var htmlCodeWithCssInlined = new PreMailer.Net.PreMailer(_htmlCode)
                .MoveCssInline(true, null, _cssCode, false, true).Html;
            _htmlInlined = new HtmlDocument();
            _htmlInlined.LoadHtml(htmlCodeWithCssInlined);
        }

        public bool Verify()
        {
            foreach (var rule in _task.Rules)
            {
                bool result = true;
                switch (rule.Type)
                {
                    case Rule.RuleType.XPathQuery:
                        result = CheckXPathQueryResult(rule);
                        break;
                    case Rule.RuleType.XPathElementStyle:
                        result = VerifyElementStyle(rule);
                        break;
                    case Rule.RuleType.XPathElementAttributes:
                        result = CheckElementAttributes(rule);
                        break;
                }
                if (!result)
                    return false;
            }
            return true;
        }

        public bool CheckXPathQueryResult(Rule rule)
        {
            var html = "<html>" + _htmlCode + "</html>";
            html = html.Replace("<br>", "<br/>");
            using (StringReader stream = new StringReader(html))
            {
                
                XPathDocument doc = new XPathDocument(stream);
                XPathNavigator navigator = doc.CreateNavigator();
                if (navigator.Evaluate(navigator.Compile(rule.Selector)).ToString() == rule.Value)
                    return true;
            }
            return false;
        }

        public bool CheckXPathPresent(Rule rule)
        {
            var elements = _htmlInlined.DocumentNode.SelectNodes(rule.Selector);
            if (elements == null)
                return false;
            return true;
        }

        public bool VerifyElementStyle(Rule rule)
        {
            //element presence
            var elements = _htmlInlined.DocumentNode.SelectNodes(rule.Selector);
            if (elements == null)
                return false;
            var ruleStyleProperties =
                new ExCSS.Parser().Parse("{" + rule.Value + "}")
                .StyleRules[0].Declarations.Properties;

            foreach (var element in elements)
            {
                var styleAttributeValue = element.GetAttributeValue("style", "");
                if (styleAttributeValue == null)
                    continue;

                var styleProperties = 
                    new ExCSS.Parser().Parse("{" + styleAttributeValue + "}")
                    .StyleRules[0].Declarations.Properties;

                bool success = true;
                // check all css properties
                foreach (var ruleStyleProperty in ruleStyleProperties)
                {
                    var actualStyleProperty =
                        styleProperties.LastOrDefault(x => x.Name == ruleStyleProperty.Name);
                    if (actualStyleProperty == null ||
                        actualStyleProperty.Term.ToString() != ruleStyleProperty.Term.ToString())
                    {
                        success = false;
                        break;
                    }
                }
                if (success)
                    return true;
            }
            return false;
        }

        private bool CheckElementAttributes(Rule rule)
        {
//        //attributes
//        if (rule.Attributes != null)
//            foreach (var attribute in rule.Attributes)
//                if (element.GetAttributeValue(attribute.Name, "") != attribute.Value)
//                    folowed = false;
            return false;
        }
    }
}