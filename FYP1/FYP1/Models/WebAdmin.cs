using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.Models
{
    public class WebAdmin
    {
        [Key]
        public int AdminId { get; set; }
        public string AdminUserName { get; set; }
        public string password { get; set; }
    }
}
