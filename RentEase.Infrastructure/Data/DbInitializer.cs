using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace RentEase.Infrastructure.Data
{
    [ExcludeFromCodeCoverage]
    public class DbInitializer
    {
        public static void InitDb(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<RentEaseContext>();

            context.Database.Migrate();
        }
    }
}
