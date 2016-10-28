using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letters.Models
{
    public class SantaDbContext : DbContext
    {
        public SantaDbContext() : base("SantasDb")
        {

        }

        public DbSet<Letter> Letters { get; set; }

        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
               .HasMany(p => p.Letters)
               .WithRequired(g => g.Author)
               .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
