using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectModule.Models
{
    namespace Testing
    {
        public class Task
        {
            public string Text { get; set; }
            public List<Rule> Rules { get; set; } 
        }
    }
}