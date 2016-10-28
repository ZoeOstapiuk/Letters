using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Letters.Models
{
    public class LetterAuthorView
    {
        public Letter Letter { get; set; }

        public Author Author { get; set; }
    }
}