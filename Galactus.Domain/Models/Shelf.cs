using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactus.Domain.Models
{
    [Table(nameof(Shelf), Schema = "Production")]
    public class Shelf
    {
        public Shelf()
        {
            Storage = new HashSet<Storage>();
        }

        [Key]
        public int ShelfId { get; set; }

        public virtual ICollection<Storage> Storage { get; set; }
    }
}
