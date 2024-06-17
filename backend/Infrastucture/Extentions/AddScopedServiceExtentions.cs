
using BussinessLogicLevel.Interfaces;
using BussinessLogicLevel.Services;
using DbLevel.Interface;
using DbLevel.Repository;
using Infrastucture.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastucture.Extentions
{
    public static class AddScopedServiceExtentions
    {
        public static IServiceCollection AddScopedService(this IServiceCollection services)
        {
            services.AddScoped<TokenGeneratorService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
