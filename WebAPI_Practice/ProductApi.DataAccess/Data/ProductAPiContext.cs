using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductAPi.Models;

namespace ProductAPi.DataAccess
{
    public class ProductAPiContext : DbContext
    {
        public ProductAPiContext (DbContextOptions<ProductAPiContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<Customer> Customers { get; set; } = default!;
    }
}
