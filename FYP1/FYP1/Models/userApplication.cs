using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FYP1.Models
{
    public class userApplication
    {
        [Key]
        public int appId { get; set; }
        public string appName { get; set; }
        public string appLogo { get; set; }
        public string appPassword { get; set; }
        
        [ForeignKey("aspUser")]
        public string Id { get; set; }
        public aspUser appUser { get; set; }
    }
}
