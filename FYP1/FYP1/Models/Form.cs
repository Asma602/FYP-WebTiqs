using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.Models
{
    public class Form
    {
        [Key]
        public int formId { get; set; }
        public string formString { get; set; }
        public string formTitle { get; set; }
       
        public int tableid { get; set; }
        public string tableName { get; set; }

    }
}
