﻿using DbLevel.Data;
using DbLevel.Settings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastucture.MappingProfilies;
using Infrastucture.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastucture.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection ServiceCollections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ShopWebApi")));

            services.AddAutoMapper(typeof(ProductProfile));
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(typeof(ProductDtoValidator).Assembly);
            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<CacheSettings>>().Value);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<ApiSettings>>().Value);

            return services;
        }
    }
}
