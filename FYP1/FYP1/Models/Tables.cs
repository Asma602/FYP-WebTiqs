using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.Models
{
    public class Tables
    {
        public int id { get; set; }
        public string tableName { get; set; }
        public List<Column> column { get; set; }
        public int foreignKey { get; set; }
    }
}
