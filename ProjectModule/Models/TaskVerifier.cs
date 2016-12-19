using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlLiteProbe
{
    public class TaskVerifier
    {
        public static bool Verify(string html, string css, Task task)
        {
            html= new PreMailer.Net.PreMailer(html).MoveCssInline(true, null, css, false, true).Html;
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            foreach (var rule in task.Rules)
            {
                //element presence
                var elements = doc.DocumentNode.SelectNodes(rule.ElementXPath);
                if (elements == null)
                    return false;
                foreach (var element in elements)
                {
                    bool folowed = true;
                    //attributes
                    if (rule.Attributes != null)
                        foreach (var attribute in rule.Attributes)
                            if (element.GetAttributeValue(attribute.Name, "") != attribute.Value)
                                folowed=false;
                    //styles
                    var styleAttributeValue = element.GetAttributeValue("style", "");
                    if (styleAttributeValue != null)
                    {
                        var styleProperties = new ExCSS.Parser().Parse("{" + styleAttributeValue + "}").StyleRules[0].Declarations.Properties;
                        if (rule.StyleProperties != null)
                            foreach (var ruleStyleProperty in rule.StyleProperties)
                            {
                                var actualStyleProperty = styleProperties.Where(x => x.Name == ruleStyleProperty.Name).LastOrDefault();
                                if (actualStyleProperty == null || actualStyleProperty.Term.ToString() != ruleStyleProperty.Value)
                                    folowed=false;
                            }
                    }
                    if (folowed)
                        return true;
                }       
            }
            return false;
        }
    }
}
