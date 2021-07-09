using Galactus.Domain;
using Galactus.Domain.Models;
using HotChocolate.Types;
using System.Linq;
using Location = Galactus.Domain.Models.Location;

namespace Galactus.Schema.Models
{
    public class InventoryReport
    {
        public InventoryReport(AdventureWorksContext context, Location location)
        {
            _context = context;
            _location = location;
        }

        readonly AdventureWorksContext _context;
        readonly Location _location;

        public int GetCountTotalInventory() =>
            _context.Inventories.Where(x => x.LocationId == _location.LocationId).Count();

        public int GetCountOccupiedInventory() =>
            _context.Inventories.Where(x => x.LocationId == _location.LocationId && x.ProductInventory.Count() > 0).Count();

        public int GetCountEmptyInventory() =>
            _context.Inventories.Where(x => x.LocationId == _location.LocationId && x.ProductInventory.Count() <= 0).Count();

        [Serial]
        [UsePaging]
        public IQueryable<Inventory> GetTotalInventory() =>
            _context.Inventories.Where(x => x.LocationId == _location.LocationId);

        [Serial]
        [UsePaging]
        public IQueryable<Inventory> GetOccupiedInventory() =>
            _context.Inventories.Where(x => x.LocationId == _location.LocationId && x.ProductInventory.Count() > 0);

        [Serial]
        [UsePaging]
        public IQueryable<Inventory> GetEmptyInventory() =>
            _context.Inventories.Where(x => x.LocationId == _location.LocationId && x.ProductInventory.Count() <= 0);
    }
}
