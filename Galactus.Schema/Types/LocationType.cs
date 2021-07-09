using AdventureWorks.Domain;
using Galactus.Schema.Models;
using HotChocolate;
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
}