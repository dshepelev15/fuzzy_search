using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchEngine.Models
{
    public class Record
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Abbr { get; set;  }
    }
}