using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Infrastructure.Repos;


namespace Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MsSql");
            services.AddDbContext<CustomerServiceDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
