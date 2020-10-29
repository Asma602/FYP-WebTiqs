using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.Models
{
    public class Databases
    {
        public int id { get; set; }
        public string name { get; set; }
        
        public List<Tables> tables { get; set; }
        public List<Relation> relations { get; set; }
    }
}
