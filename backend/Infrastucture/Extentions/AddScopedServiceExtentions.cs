using BussinessLogicLevel.Interfaces;
using BussinessLogicLevel.Services;
using DbLevel;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.Repository;
using Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastucture.Extentions
{
    public static class AddScopedServiceExtentions
    {
        public static IServiceCollection AddScopedService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            var repositoryAssembly = typeof(IBase).Assembly;
            var repositoryTypes = repositoryAssembly.GetTypes()
                .Where(t => typeof(IBase).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .ToList();
            // IBase - я сущностей
            foreach (var repoType in repositoryTypes)
            {
                var interfaceTypes = repoType.GetInterfaces()
                    .Where(i => i != typeof(IBase) && typeof(IBase).IsAssignableFrom(i))
                    .ToList();

                if (interfaceTypes.Any())
                {
                    foreach (var interfaceType in interfaceTypes)
                    {
                        services.AddScoped(interfaceType, repoType);
                    }
                }
            }
            
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<TokenGeneratorService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProductStorageService, ProductStorageService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPromoCodeService, PromoCodeService>();
            return services;
        }
    }
}
