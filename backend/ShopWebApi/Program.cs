using BussinessLogicLevel.Interfaces;
using BussinessLogicLevel.Services;
using DbLevel.Data;
using DbLevel.Interface;
using DbLevel.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastucture;
using Infrastucture.Services;
using Infrastucture.Validators;
using Microsoft.EntityFrameworkCore;
using Infrastucture.Extentions;
using ShopWebApi;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<TokenGeneratorService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddAuthorizationService(builder.Configuration);
builder.Services.AddCustomAuthorization();
builder.Services.AddCustomIdentity();
builder.Services.AddScopedService();
builder.Services.ServiceCollections(builder.Configuration);
builder.Services.AddLoggingService();


var app = builder.Build();

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
