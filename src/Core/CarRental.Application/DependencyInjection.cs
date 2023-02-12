using CarRental.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CarRental.Application
{
    public static class DependencyInjection
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<ICustomerLoyaltyService, CustomerLoyaltyService>();
            services.AddScoped<IRentPriceService, RentPriceService>();
        }
    }
}
