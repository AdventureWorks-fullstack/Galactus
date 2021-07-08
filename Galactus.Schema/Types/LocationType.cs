using System.Linq;
using AdventureWorks.Domain;
using AdventureWorks.Domain.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Location = AdventureWorks.Domain.Models.Location;

namespace Galactus.Schema.Types
{
    public class LocationType : ObjectType<Location>
    {
        protected override void Configure(IObjectTypeDescriptor<Location> descriptor)
        {
            base.Configure(descriptor);

            descriptor.BindFieldsImplicitly();

            descriptor.Field(f => f.LocationId).IsProjected(true);

            descriptor.Field("inventory")
                .ResolveWith<Resolver>(f => f.InventoryReport(default!, default!));
        }

        private class Resolver
        {
            public InventoryReport InventoryReport([Service] AdventureWorksContext context, [Parent] Location location)
            {
                return new InventoryReport(context, location);
            }
        }
    }

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