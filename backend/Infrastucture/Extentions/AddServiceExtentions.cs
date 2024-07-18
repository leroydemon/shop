using BussinessLogicLevel.Interfaces;
using BussinessLogicLevel.Services;
using DbLevel;
using DbLevel.Interfaces;
using Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastucture.Extentions
{
    public static class AddServiceExtentions
    {
        public static IServiceCollection AddScopedService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            var repositoryAssembly = typeof(IEntity).Assembly;
            var repositoryTypes = repositoryAssembly.GetTypes()
                .Where(t => typeof(IEntity).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .ToList();


            foreach (var repoType in repositoryTypes)
            {
                var interfaceTypes = repoType.GetInterfaces()
                    .Where(i => i != typeof(IEntity) && typeof(IEntity).IsAssignableFrom(i))
                    .ToList();

                if (interfaceTypes.Any())
                {
                    foreach (var interfaceType in interfaceTypes)
                    {
                        services.AddScoped(interfaceType, repoType);
                    }
                }
            }

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
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPostOfficeService, PostOfficeService>();


            return services;
        }
    }
}

