using SportsEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEcommerce.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        // Tables related to tables in sql
        public DbSet<Product> Products { get; set; }
    }
}
