using Galactus.Domain.Models;
using Galactus.Schema.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactus.Schema.Mutations
{
    public partial class Mutation
    {
        public async Task<AddShelvesMutationPayload> AddShelvesMutation(AddShelvesMutationInput input)
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
