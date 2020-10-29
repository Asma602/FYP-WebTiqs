using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.Models
{
    public class Column
    {
        public int id { get; set; }
        public string name { get; set; }
        public string dataType { get; set; }
        public int minLength { get; set; }
        public int maxLength { get; set; }
        public int required { get; set; }
       
    }
  
}
