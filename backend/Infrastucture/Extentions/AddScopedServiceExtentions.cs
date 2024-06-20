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
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
