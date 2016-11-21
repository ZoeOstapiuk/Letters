using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Letters.Models
{
    public class LetterAuthorModel
    {
        public string Email { get; set; }
        
        public string Letter { get; set; }

        public DateTime Date { get; set; }
    }
}
