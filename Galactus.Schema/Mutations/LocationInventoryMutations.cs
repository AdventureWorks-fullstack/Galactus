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
                var locationInventory = new List<LocationInventory>();
                var locationInventoryId = "";

                for (int nbShelves = 0; nbShelves < input.Shelves.Length; nbShelves++)
                {
                    for (int nbRows = 0; nbRows < input.Shelves[nbShelves].Rows.Length; nbRows++)
                    {
                        for (int nbBins = 0; nbBins < input.Shelves[nbShelves].Rows[nbRows].Bins; nbBins++)
                        {
                            // A-01-01-01
                            locationInventoryId =
                                $"{input.Prefix}-" +
                                $"{(nbShelves + 1).ToString("00")}-" +
                                $"{(nbRows + 1).ToString("00")}-" +
                                $"{(nbBins + 1).ToString("00")}";

                            locationInventory.Add(new LocationInventory
                            {
                                LocationInventoryId = locationInventoryId,
                                LocationId = input.LocationId,
                            });
                        }
                    }
                }

                await context.LocationInventories.AddRangeAsync(locationInventory);
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
        Shelf[] Shelves
        );

    public record Shelf(
        Row[] Rows
        );

    public record Row(
        byte Bins
        );

    public class AddShelfPayload
    {
        public AddShelfPayload(List<LocationInventory> locationInventories)
        {
            CreatedLocationInventories = locationInventories;
        }

        public List<LocationInventory> CreatedLocationInventories { get; set; }
    }
}
