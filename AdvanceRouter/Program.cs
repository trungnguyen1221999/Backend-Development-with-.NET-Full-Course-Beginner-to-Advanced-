var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/users/{userId:guid}/posts/{slug}", (Guid userId, string slug) =>
{
    return Results.Created($"/users/{userId}/posts/{slug}", new { UserId = userId, Slug = slug });
});


app.MapGet("/products/{id:guid}", (Guid id) =>
{
    return Results.Ok(new { Id = id });
});
app.Run();
