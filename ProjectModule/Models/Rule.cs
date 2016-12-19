using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sqlLiteProbe
{
    public class Rule
    {
        public string ElementXPath { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<StyleProperty> StyleProperties { get; set; }       
    }
    public class Attribute
    {
        public string Name { set; get; }
        public string Value { set; get; }
    }
    public class StyleProperty
    {
        public string Name { set; get; }
        public string Value { set; get; }
    }
}