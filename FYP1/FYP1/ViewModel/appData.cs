using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.Models
{
    public class appData
    {
        public List<string> cols { get; set; }
        public string tableName { get; set; }
        public string dbName { get; set; }
        public List<string> attribute{ get; set; }
    }

}
