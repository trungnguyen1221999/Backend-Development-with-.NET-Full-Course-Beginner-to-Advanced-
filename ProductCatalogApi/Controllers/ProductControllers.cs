namespace ProductCatalogApi.Controllers;

using ProductCatalogApi.Models;
using ProductCatalogApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Products>> GetAll() => _service.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Products> GetById(string id)
    {
        var product = _service.GetById(id);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public ActionResult<Products> Create(Products product)
    {
        var created = _service.Create(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public ActionResult<Products> Update(string id, Products updatedProduct)
    {
        var product = _service.Update(id, updatedProduct);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpPatch("{id}")]
    public ActionResult<Products> PartialUpdate(string id, JsonPatchDocument<Products> patchDoc)
    {
        var product = _service.PartialUpdate(id, patchDoc);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        return _service.Delete(id) ? NoContent() : NotFound();
    }
}
