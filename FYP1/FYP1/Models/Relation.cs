using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.Models
{
    public class Relation
    {
        public int pkey { get; set; }
        public string pTableName { get; set; }
        public int fKey { get; set; }
        public string fTableName { get; set; }
        public string relationType { get; set; }
    }
}
