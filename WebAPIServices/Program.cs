global using WebAPIServices.Models;
using Microsoft.Extensions.Caching.Memory;
using WebAPIServices.Services.ProductServices;
using WebAPIServices.Services.SellerServices;
using WebAPIServices.Services.SuperHeroService;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISellerService, SellerService>();
var cache = new MemoryCache(new MemoryCacheOptions());
builder.Services.AddSingleton<IMemoryCache>(cache);

builder.Services.AddSingleton<IProductService>(provider =>
{
    var productService = new ProductService();
    productService.SetCache(provider.GetRequiredService<IMemoryCache>());
    return productService;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
