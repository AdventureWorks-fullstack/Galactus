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
    internal class InventoryHistoryConfiguration : IEntityTypeConfiguration<InventoryHistory>
    {
        public void Configure(EntityTypeBuilder<InventoryHistory> builder)
        {
            builder
                .HasOne(ih => ih.ProductInventory)
                .WithMany(pp => pp.InventoryHistory)
                .HasForeignKey("LocationId", "ProductId")
                .IsRequired(false);

        }
    }
}
