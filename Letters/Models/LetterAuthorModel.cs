using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Letters.Models
{
    public class LetterAuthorModel
    {
        public Letter Letter { get; set; }

        public ApplicationUser Author { get; set; }
    }
}