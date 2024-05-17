using Microsoft.Extensions.Caching.Memory;
using WebAPIServices.Data;
using WebAPIServices.Services.ProductServices;
using WebAPIServices.Services.SellerServices;
using WebAPIServices.Services.SuperHeroService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryrService, CategoryService>();
var cache = new MemoryCache(new MemoryCacheOptions());
builder.Services.AddSingleton<IMemoryCache>(cache);

builder.Services.AddScoped<IProductService, ProductService>(provider =>
{
    var context = provider.GetRequiredService<DataContext>();
    var cache = provider.GetRequiredService<IMemoryCache>();
    return new ProductService(context, cache);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Thêm middleware logging vào pipeline
app.Use(async (context, next) =>
{
    // Xử lý trước khi gọi middleware tiếp theo
    Console.WriteLine($"Request: {context.Request.Path}");
    await next.Invoke();
    // Xử lý sau khi gọi middleware tiếp theo
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
