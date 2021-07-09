using Microsoft.EntityFrameworkCore;

namespace Galactus.Domain
{
    public static class AdventureWorksContextFactory
    {
        public static AdventureWorksContext GetContext(string conString)
        {
            var builder = new DbContextOptionsBuilder<AdventureWorksContext>();
            builder.UseSqlServer(conString);

            var context = new AdventureWorksContext(builder.Options);

            return context;
        }
    }
}