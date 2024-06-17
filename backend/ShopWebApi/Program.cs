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

var builder = WebApplication.CreateBuilder(args);

// Create separate class with extension method and move all logic to sepatate methods and call it in this class

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
