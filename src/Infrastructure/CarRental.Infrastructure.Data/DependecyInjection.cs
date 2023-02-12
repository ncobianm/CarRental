using CarRental.Application.Interfaces.Repositories;
using CarRental.Infrastructure.Data.Data;
using CarRental.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Infrastructure.Data
{
    public static class DependencyInjection
    {
        public static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DataConnection"
            ), b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
