using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.Models
{
    public class dbTables
    {
        [Key]
        public int tableId { get; set; }
        public string tablename { get; set; }
        [ForeignKey("appDatabase")]
        public int dbId { get; set; }
        public  appDatabase appDatabase { get; set; }
    }
}
