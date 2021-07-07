using AdventureWorks.Domain;
using AdventureWorks.Domain.Models;
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
                var locationInventoryId = "";

                for (int nbShelves = 0; nbShelves < input.Shelves.Length; nbShelves++)
                {
                    for (int nbRows = 0; nbRows < input.Shelves[nbShelves].Length; nbRows++)
                    {
                        for (int nbBins = 0; nbBins < input.Shelves[nbShelves][nbRows]; nbBins++)
                        {
                            // A-01-01-01
                            locationInventoryId =
                                $"{input.Prefix}-" +
                                $"{(nbShelves + 1).ToString("00")}-" +
                                $"{(nbRows + 1).ToString("00")}-" +
                                $"{(nbBins + 1).ToString("00")}";

                            locationInventory.Add(new Inventory
                            {
                                InventoryId = locationInventoryId,
                                LocationId = input.LocationId,
                            });
                        }
                    }
                }

                await context.Inventories.AddRangeAsync(locationInventory);
                await context.SaveChangesAsync();

                return new AddShelfPayload(locationInventory);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public record AddShelfInput(
        short LocationId,
        string Prefix,
        byte[][] Shelves
        );

    public class AddShelfPayload
    {
        public AddShelfPayload(List<Inventory> locationInventories)
        {
            CreatedInventories = locationInventories;
        }

        public List<Inventory> CreatedInventories { get; set; }
    }
}
