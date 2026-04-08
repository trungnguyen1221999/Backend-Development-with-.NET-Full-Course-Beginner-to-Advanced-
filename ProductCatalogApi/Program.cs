var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
