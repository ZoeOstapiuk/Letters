using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Letters.Models
{
    public class Letter
    {
        public int LetterId { get; set; }

        public string Content { get; set; }

        // public Author Author { get; set; }
    }
}