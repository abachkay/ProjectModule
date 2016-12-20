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
                        result = VerifyXPathQueryResult(rule);
                        break;
                    case Rule.RuleType.XPathElementStyle:
                        result = VerifyElementStyle(rule);
                        break;
                    case Rule.RuleType.XPathElementAttributes:
                        result = VerifyElementAttributes(rule);
                        break;
                }
                if (!result)
                    return false;
            }
            return true;
        }

        public bool VerifyXPathQueryResult(Rule rule)
        {
            var html = "<html>" + _htmlCode + "</html>";
            using (StringReader stream = new StringReader(html))
            {
                
                XPathDocument doc = new XPathDocument(stream);
                XPathNavigator navigator = doc.CreateNavigator();
                if (navigator.Evaluate(navigator.Compile(rule.Selector)).ToString() == rule.Value)
                    return true;
            }
            return false;
        }

        public bool VerifyXPathPresent(Rule rule)
        {
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

        private bool VerifyElementAttributes(Rule rule)
        {
            //element presence
            var elements = _html.DocumentNode.SelectNodes(rule.Selector);
            if (elements == null)
                return false;
            var attributesHtml = new HtmlDocument();
            attributesHtml.LoadHtml("<div " + rule.Value + "></div>");
            var ruleAttributes = attributesHtml.DocumentNode.FirstChild.Attributes;

            foreach (var element in elements)
            {
                var elementAttributes = element.Attributes;
                if (elementAttributes == null)
                    continue;

                bool success = true;
                // check allattributes
                foreach (var ruleAttribute in ruleAttributes)
                {
                    var actualAttribute =
                        elementAttributes.LastOrDefault(x => x.Name == ruleAttribute.Name);
                    if (actualAttribute == null ||
                        actualAttribute.Value != ruleAttribute.Value)
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
    }
}