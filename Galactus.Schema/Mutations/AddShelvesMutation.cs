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
        public async Task<AddShelvesMutationPayload> AddShelvesMutation([Service] AdventureWorksContext context, int locationId)
        {
            var prefixes = new string[]
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
            };
            var shelves = new int[][]
            {
                new int[]{1, 12, 12, 12, 1},
                new int[]{1, 12, 12, 12, 1},
                new int[]{1, 4, 4, 4, 1},
                new int[]{1, 4, 4, 4, 1},
                new int[]{1, 1, 1, 1, 1},
                new int[]{1, 1, 1, 1, 1},
            };
            var createdShelves = new List<Shelf>();

            try
            {
                for (int nbShelves = 0; nbShelves < prefixes.Length; nbShelves++)
                {
                    var createdStorage = new List<Storage>();

                    for (int nbColumns = 0; nbColumns < shelves.Length; nbColumns++)
                    {
                        for (int nbRows = 0; nbRows < shelves[nbColumns].Length; nbRows++)
                        {
                            for (int nbBins = 0; nbBins < shelves[nbColumns][nbRows]; nbBins++)
                            {
                                string name =
                                    $"{prefixes[nbShelves]}-" +
                                    $"{(nbColumns + 1).ToString("00")}-" +
                                    $"{(nbRows + 1).ToString("00")}-" +
                                    $"{(nbBins + 1).ToString("00")}";

                                Console.WriteLine(name);

                                createdStorage.Add(new Storage { StorageName = name, LocationId = (short)locationId });
                            }
                        }
                    }

                    createdShelves.Add(new Shelf { Storage = createdStorage });
                }

                await context.Shelves.AddRangeAsync(createdShelves);
                await context.SaveChangesAsync();

                return new AddShelvesMutationPayload(createdShelves);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AddShelvesMutationPayload> AddShelfMutation(AddShelvesMutationInput input)
        {
            await Task.Delay(1);
            return new AddShelvesMutationPayload(new List<UserError>() { new UserError("Not implemented", "500") });
        }
    }

    public record AddShelvesMutationInput(
        int locationId,
        string prefix,
        int[][] shelves
        );

    public class AddShelvesMutationPayload : Payload
    {
        public AddShelvesMutationPayload(ICollection<Shelf> createdShelves)
        {
            CreatedShelves = createdShelves;
        }

        public AddShelvesMutationPayload(IReadOnlyList<UserError> userErrors) : base(userErrors) { }

        public ICollection<Shelf>? CreatedShelves { get; set; }
    }
}
