var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestMethod
        | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPath
        | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseStatusCode
        | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.Duration;
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine("logic1 before");
    await next.Invoke();
    Console.WriteLine("logic1 after");
});

app.Use(async (context, next) =>
{
    Console.WriteLine("logic2 before");
    await next.Invoke();
    Console.WriteLine("logic2 after");
});


app.Use(async (context, next) =>
{
    Console.WriteLine("logic3 before");
    await next.Invoke();
    Console.WriteLine("logic3 after");
});


app.UseHttpLogging();

app.MapGet("/", () => "Hello World!");
app.MapGet("/users/{id}", (string id) => $"User ID: {id}");
app.Run();
