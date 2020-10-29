using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.ViewModel
{
    public class EditAppRecord
    {
        public string tableName { get; set; }
        public string dbName { get; set; }
        public string[] attribute { get; set; }
        public string [] colsValue { get; set; }
        public string [] dataType { get; set; }
    }
}
