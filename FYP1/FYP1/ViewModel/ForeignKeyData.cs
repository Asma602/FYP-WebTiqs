using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.ViewModel
{
    public class ForeignKeyData
    {
        public List<string> parentDataName { get; set; }
        public  List<int> parentDataId { get; set; }
        public string tableName { get; set; }
    }
}
