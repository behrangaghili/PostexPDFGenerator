using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Postex.receipt.Infrastrucre.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
        private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
