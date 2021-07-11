using Galactus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactus.Domain.Configuration
{
    internal class StorageConfiguration : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> builder)
        {
            builder
                .HasOne(a => a.Shelf)
                .WithMany(b => b.Storage)
                .HasForeignKey(nameof(Storage.ShelfId))
                .IsRequired(false);

            builder
                .HasMany(a => a.ProductInventory)
                .WithMany(b => b.Storage)
                .UsingEntity(c => c.ToTable("ProductInventoryStorage", "Production"));
        }
    }
}
