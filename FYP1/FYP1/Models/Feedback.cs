using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.Models
{
    public class Feedback
    {
        [Key]
        public int feedbackId { get; set; }

        public string userName { get; set; }
        public string comments { get; set; }

        [ForeignKey("aspUser")]
        public string Id { get; set; }

    }
}
