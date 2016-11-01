using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Letters.Models
{
    public class Author
    {
        public Author()
        {
            Letters = new List<Letter>();
        }

        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }
        
        [Required]
        public string Address { get; set; }

        [RegularExpression(@"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@([a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$",
            ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        public int Age { get; set; }
        
        public ICollection<Letter> Letters { get; set; }
    }
}
