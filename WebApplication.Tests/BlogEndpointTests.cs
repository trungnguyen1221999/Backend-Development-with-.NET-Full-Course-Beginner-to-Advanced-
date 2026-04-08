using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApplication.Tests;

// WebApplicationFactory<Program> khởi động app thật trong memory,
// IClassFixture giúp dùng chung 1 instance factory cho toàn bộ test trong class (nhanh hơn)
public class BlogEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BlogEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    // ---------------------------------------------------------------
    // GET /
    // ---------------------------------------------------------------

    [Fact]
    public async Task Get_Root_ReturnsHelloWorld()
    {
        // Act
        var response = await _client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Hello World!", content);
    }

    // ---------------------------------------------------------------
    // GET /blogs
    // ---------------------------------------------------------------

    [Fact]
    public async Task GetBlogs_ReturnsOkWithTwoDefaultBlogs()
    {
        // Act
        var response = await _client.GetAsync("/blogs");
        var blogs = await response.Content.ReadFromJsonAsync<List<Blog>>();

        // Assert
        // dùng >= 2 vì các test khác trong cùng class có thể thêm blog (shared in-memory state)
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(blogs);
        Assert.True(blogs.Count >= 2);
    }

    // ---------------------------------------------------------------
    // GET /blogs/{id}
    // ---------------------------------------------------------------

    [Fact]
    public async Task GetBlogById_WithValidId_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/blogs/0");
        var blog = await response.Content.ReadFromJsonAsync<Blog>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(blog);
        Assert.Equal("First Blog", blog.Title);
    }

    [Fact]
    public async Task GetBlogById_WithInvalidId_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync("/blogs/99");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetBlogById_WithNegativeId_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync("/blogs/-1");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // ---------------------------------------------------------------
    // POST /blogs
    // ---------------------------------------------------------------

    [Fact]
    public async Task PostBlog_WithValidData_ReturnsCreated()
    {
        // Arrange
        var newBlog = new Blog { Title = "Test Blog", Body = "Test Body" };

        // Act
        var response = await _client.PostAsJsonAsync("/blogs", newBlog);
        var created = await response.Content.ReadFromJsonAsync<Blog>();

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(created);
        Assert.Equal("Test Blog", created.Title);
        Assert.NotNull(response.Headers.Location); // phải trả về Location header
    }

    // ---------------------------------------------------------------
    // PUT /blogs/{id}
    // ---------------------------------------------------------------

    [Fact]
    public async Task PutBlog_WithValidId_ReturnsOkWithUpdatedData()
    {
        // Arrange
        var updated = new Blog { Title = "Updated Title", Body = "Updated Body" };

        // Act
        var response = await _client.PutAsJsonAsync("/blogs/0", updated);
        var result = await response.Content.ReadFromJsonAsync<Blog>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal("Updated Title", result.Title);
        Assert.Equal("Updated Body", result.Body);
    }

    [Fact]
    public async Task PutBlog_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var updated = new Blog { Title = "X", Body = "X" };

        // Act
        var response = await _client.PutAsJsonAsync("/blogs/99", updated);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // ---------------------------------------------------------------
    // PATCH /blogs/{id}
    // ---------------------------------------------------------------

    [Fact]
    public async Task PatchBlog_UpdateTitleOnly_ReturnsOkWithNewTitle()
    {
        // Arrange — chỉ gửi Title, Body để rỗng
        var patch = new Blog { Title = "Patched Title", Body = "" };

        // Act
        var response = await _client.PatchAsJsonAsync("/blogs/1", patch);
        var result = await response.Content.ReadFromJsonAsync<Blog>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal("Patched Title", result.Title);
    }

    [Fact]
    public async Task PatchBlog_UpdateBodyOnly_ReturnsOkWithNewBody()
    {
        // Arrange — chỉ gửi Body, Title để rỗng
        var patch = new Blog { Title = "", Body = "Patched Body" };

        // Act
        var response = await _client.PatchAsJsonAsync("/blogs/1", patch);
        var result = await response.Content.ReadFromJsonAsync<Blog>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal("Patched Body", result.Body);
    }

    [Fact]
    public async Task PatchBlog_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var patch = new Blog { Title = "X", Body = "" };

        // Act
        var response = await _client.PatchAsJsonAsync("/blogs/99", patch);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // ---------------------------------------------------------------
    // DELETE /blogs/{id}
    // ---------------------------------------------------------------

    [Fact]
    public async Task DeleteBlog_WithValidId_ReturnsNoContent()
    {
        // Arrange — thêm blog mới trước để xóa (tránh ảnh hưởng test khác)
        var blog = new Blog { Title = "To Delete", Body = "Will be deleted" };
        var postResponse = await _client.PostAsJsonAsync("/blogs", blog);
        var location = postResponse.Headers.Location!.ToString(); // vd: /blogs/2

        // Act
        var response = await _client.DeleteAsync(location);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteBlog_WithInvalidId_ReturnsNotFound()
    {
        // Act
        var response = await _client.DeleteAsync("/blogs/99");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
