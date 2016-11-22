using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Letters.Models
{
    public class Letter
    {
        public int LetterId { get; set; }

        [Required(ErrorMessage = "Write something!")]
        public string Content { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime DateTimeCreation { get; set; }
    }
}