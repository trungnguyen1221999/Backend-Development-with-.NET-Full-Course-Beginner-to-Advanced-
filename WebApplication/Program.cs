var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var blogs = new List<Blog>()
{
    new Blog { Title = "First Blog", Body = "This is the body of the first blog." },
    new Blog { Title = "Second Blog", Body = "This is the body of the second blog." }
};

app.MapGet("/", () => "Hello World!");
app.MapGet("/blogs", () => blogs);
app.MapGet("/blogs/{id}", (int id) => {
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    return Results.Ok(blogs[id]);
});

app.MapPost("/blogs", (Blog blog) => {
    blogs.Add(blog);
    return Results.Created($"/blogs/{blogs.Count - 1}", blog);
});

app.MapPut("/blogs/{id}", (int id, Blog updatedBlog) => {
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    blogs[id] = updatedBlog;
    return Results.Ok(updatedBlog);
});

app.MapPatch("/blogs/{id}", (int id, Blog updatedBlog) => {
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    var blog = blogs[id];
    if (!string.IsNullOrEmpty(updatedBlog.Title))
    {
        blog.Title = updatedBlog.Title;
    }
    if (!string.IsNullOrEmpty(updatedBlog.Body))
    {
        blog.Body = updatedBlog.Body;
    }
    return Results.Ok(blog);
});

app.MapDelete("/blogs/{id}", (int id) => {
    if (id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }
    blogs.RemoveAt(id);
    return Results.NoContent();
});

app.Run();

public class Blog
{
    public required string Title { get; set; }
    public required string Body { get; set; }

};

