using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Letters.Models
{
    public class LettersDbContext : DbContext
    {
        public DbSet<Letter> Letters { get; set; }

        // public DbSet<Author> Authors { get; set; }
    }
}