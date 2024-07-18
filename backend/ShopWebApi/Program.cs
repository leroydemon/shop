using Infrastucture.Services;
using Infrastucture.Extentions;
using Authorization;
using DbLevel;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorizationService(builder.Configuration);
builder.Services.AddCustomAuthorization();
builder.Services.AddCustomIdentity();
builder.Services.AddScopedService();
builder.Services.ServiceCollections(builder.Configuration);
builder.Services.AddLoggingService();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();



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
