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
            services.AddScoped<TokenGeneratorService>();
            services.AddScoped(typeof(DbLevel.Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Brand>, Repository<Brand>>();
            services.AddScoped<IRepository<Cart>, Repository<Cart>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<ProductStorage>, Repository<ProductStorage>>();
            services.AddScoped<IRepository<Storage>, Repository<Storage>>();
            services.AddScoped<IRepository<PromoCode>, Repository<PromoCode>>();

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
