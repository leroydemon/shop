using BussinessLogicLevel.FluentValidators;
using BussinessLogicLevel.Interfaces;
using BussinessLogicLevel.Services;
using DbLevel.Data;
using DbLevel.Interface;
using DbLevel.Models;
using DbLevel.Repository;
<<<<<<< HEAD:ShopWebApi/Program.cs
<<<<<<< HEAD
using FluentValidation;
=======
using Microsoft.AspNetCore.Authentication.Cookies;
=======
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastucture;
using Infrastucture.Services;
using Infrastucture.Validators;
>>>>>>> feature/addederrorhandlingservice:backend/ShopWebApi/Program.cs
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
>>>>>>> feature/validationfixed
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopWebApi;
using ShopWebApi.AuthConfig;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<TokenGeneratorService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
<<<<<<< HEAD:ShopWebApi/Program.cs
<<<<<<< HEAD
builder.Services.AddScoped<IValidator<Brand>, BrandValidator>();
builder.Services.AddScoped<IValidator<Cart>, CartValidator>();
builder.Services.AddScoped<IValidator<ProductStorage>, ProductStorageValidator>();
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
builder.Services.AddScoped<IValidator<Storage>, StorageValidator>();
builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<Category>, CategoryValidator>();

// Add services to the container.
=======
=======

>>>>>>> feature/addederrorhandlingservice:backend/ShopWebApi/Program.cs
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false; 
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
>>>>>>> feature/validationfixed


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.UseSecurityTokenValidators = true;
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidAudience = AuthOptions.AUDIENCE,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
        };
    }
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Àdmin"));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ShopWebApi")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(ProductDtoValidator).Assembly);
builder.Services.AddLogging();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRoles.InitializeAsync(services);
}
app.UseMiddleware<ErrorHandlingService>();
app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();


app.Run();
