using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Galactus.Domain.Models
{
    [Table(nameof(Storage), Schema = "Production")]
    public class Storage
    {
        public Storage()
        {
            ProductInventory = new HashSet<ProductInventory>();
            StorageHistory = new HashSet<StorageHistory>();
        }

        [Key]
        public int StorageId { get; set; }

        public string StorageName { get; set; }
        public StorageType StorageType { get; set; }

        public short LocationId { get; set; }
        public int ShelfId { get; set; }

        public virtual Location Location { get; set; }
        public virtual Shelf Shelf { get; set; }
        public virtual ICollection<StorageHistory> StorageHistory { get; set; }
        public virtual ICollection<ProductInventory> ProductInventory { get; set; }
    }
}
