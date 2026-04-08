using ProductCatalogApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
