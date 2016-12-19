using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sqlLiteProbe
{
    public class Task
    {
        public string Description { set; get; }
        public ICollection<Rule> Rules { set; get; }
    }
}