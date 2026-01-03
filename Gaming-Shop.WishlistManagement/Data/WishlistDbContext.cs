using Gaming_Shop.ShopManagement.Entities;
using Gaming_Shop.WishlistManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.WishlistManagement.Data
{
    public class WishlistDbContext : DbContext
    {
        public WishlistDbContext(DbContextOptions<WishlistDbContext> options)
            : base(options) { }

        public DbSet<Wishlist> Wishlists => Set<Wishlist>();
        public DbSet<WishlistItems> WishlistItems => Set<WishlistItems>();
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WishlistDbContext).Assembly);
        }
    }

}
