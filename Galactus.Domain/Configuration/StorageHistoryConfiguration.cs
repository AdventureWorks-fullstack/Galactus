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
    internal class StorageHistoryConfiguration : IEntityTypeConfiguration<StorageHistory>
    {
        public void Configure(EntityTypeBuilder<StorageHistory> builder)
        {
            //builder
            //    .HasOne(ih => ih.ProductInventory)
            //    .WithMany(pp => pp.StorageHistory)
            //    .HasForeignKey(nameof(StorageHistory.LocationId), nameof(StorageHistory.ProductId))
            //    .IsRequired(false);

        }
    }
}
