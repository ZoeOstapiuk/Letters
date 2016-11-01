using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letters.Models
{
    public class SantaDbInitializer : DropCreateDatabaseIfModelChanges<SantaDbContext>
    {
        protected override void Seed(SantaDbContext context)
        {
            context.Letters.Add(new Letter
            {
                Content = "I love Roman!",
                Author = new Author
                {
                    Name = "Zoe",
                    Email = "ostapyukzoya@outlook.com",
                    Address = "Ukraine",
                    Age = 18
                }
            });

            base.Seed(context);
        }
    }
}
