using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postex.receipt.Infrastrucre.Data;
using Postex.receipt.Infrastrucre.Repositories;
using Postex.SharedKernel.Interfaces;

namespace Postex.receipt.Infrastrucre.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("receipt_db"));
                options.EnableSensitiveDataLogging();
            });

            services.AddRepositories(configuration);
            return services;
        }
        private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IWriteRepository<>), typeof(EFRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(EFRepository<>));
        }
    }
}
