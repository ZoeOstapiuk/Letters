using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Letters.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Letters = new List<Letter>();
        }
        
        public virtual ICollection<Letter> Letters { get; set; }
    }
}