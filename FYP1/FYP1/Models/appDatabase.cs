using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.Models
{
    public class appDatabase
    {
        [Key]
        public int dbId { get; set; }
        public string  dbName { get; set; }
        
      

        [ForeignKey("userApplication")]
        public int appId { get; set; }
        public  userApplication userApplication { get; set; }

    }
}
