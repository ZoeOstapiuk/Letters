using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Letters.Models
{
    public class Letter
    {
        public int LetterId { get; set; }

        [Required(ErrorMessage = "Write something!")]
        public string Content { get; set; }
        
        public virtual ApplicationUser Author { get; set; }
    }
}