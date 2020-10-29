using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.Models
{
    public class Query
    {
        [Key]
        public int queryId { get; set; }
        public string queryUserName { get; set; }

        public string queryUserEmail { get; set; }
        public string userMessage { get; set; }

        [ForeignKey("aspUser")]
        public string Id { get; set; }

    }
}
