using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letters.Models
{
    public class LetterModel
    {
        [Required]
        public string Letter { get; set; }
    }
}
