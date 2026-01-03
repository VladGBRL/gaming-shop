using Gaming_Shop.WishlistManagement.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.WishlistManagement.Data.Configurations
{
    public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItems>
    {
        public void Configure(EntityTypeBuilder<WishlistItems> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.ProductId);
        }
    }
}
