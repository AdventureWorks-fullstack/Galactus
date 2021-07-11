using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Galactus.Domain.Models
{
    [Table(nameof(StorageHistory), Schema = "Production")]
    public class StorageHistory
    {
        // PK 
        public int StorageHistoryId { get; set; }

        // FK
        public int StorageId { get; set; }
        public short LocationId { get; set; }
        public int ProductId { get; set; }
        public int? MovedHereByEmployeeId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Storage Storage { get; set; }
        [ForeignKey("ProductId, LocationId")] // Manual hack to make ef get the relationship
        public virtual ProductInventory ProductInventory { get; set; }
        public virtual Employee MovedHereByEmployee { get; set; }
    }
}
