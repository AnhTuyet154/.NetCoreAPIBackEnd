using WebAPIServices.Data;
using WebAPIServices.Services.CategoryServices;
using WebAPIServices.Services.ProductServices;
using Microsoft.Net.Http.Headers;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Thêm middleware Response Caching vào pipeline
app.UseResponseCaching();

// Configure the response cache middleware
app.Use(async (context, next) =>
{
    // Xác định các yêu cầu mà bạn muốn cache (ví dụ: tất cả các phản hồi thành công)
    if (context.Request.Method == HttpMethods.Get && context.Response.StatusCode == StatusCodes.Status200OK)
    {
        context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
        {
            Public = true, // Cacheable by clients and shared (proxy caching)
            MaxAge = TimeSpan.FromMinutes(5) // Thời gian sống của cache
        };
    }

    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
