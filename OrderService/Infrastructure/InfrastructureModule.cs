using Application.Interfaces;
using Infrastructure.InfraRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MsSql");
            services.AddDbContext<OrderServiceDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
