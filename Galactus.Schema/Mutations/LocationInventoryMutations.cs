using Galactus.Domain;
using Galactus.Domain.Models;
using Galactus.Schema.Common;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactus.Schema.Mutations
{
    public partial class Mutation
    {
        public async Task<AddShelfPayload> AddShelfMutation(
            [Service] AdventureWorksContext context,
            AddShelfInput input)
        {
            try
            {
                var locationInventory = new List<Inventory>();
                var inventoryName = "";

                for (int nbShelves = 0; nbShelves < input.Shelves.Length; nbShelves++)
                {
                    for (int nbRows = 0; nbRows < input.Shelves[nbShelves].Length; nbRows++)
                    {
                        for (int nbBins = 0; nbBins < input.Shelves[nbShelves][nbRows]; nbBins++)
                        {
                            // A-01-01-01
                            inventoryName =
                                $"{input.Prefix}-" +
                                $"{(nbShelves + 1).ToString("00")}-" +
                                $"{(nbRows + 1).ToString("00")}-" +
                                $"{(nbBins + 1).ToString("00")}";

                            locationInventory.Add(new Inventory
                            {
                                InventoryName = inventoryName,
                                LocationId = input.LocationId,
                            });
                        }
                    }
                }

                await context.Inventories.AddRangeAsync(locationInventory);
                await context.SaveChangesAsync();

                return new AddShelfPayload(locationInventory);
            }
            catch
            {
                // TODO log ex
                throw;
            }
        }
    }

    public record AddShelfInput(
        short LocationId,
        string Prefix,
        byte[][] Shelves
        );

    public class AddShelfPayload : Payload
    {
        public AddShelfPayload(List<Inventory> locationInventories)
        {
            CreatedInventories = locationInventories;
        }
        public AddShelfPayload(IReadOnlyList<UserError> errors) : base(errors) { }

        public List<Inventory>? CreatedInventories { get; set; }
    }
}
