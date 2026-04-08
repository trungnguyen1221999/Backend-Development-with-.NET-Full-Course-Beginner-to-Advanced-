namespace ProductCatalogApi.Controllers;

using ProductCatalogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;


[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private static List<Products> products = new List<Products>();

    //Get All
    [HttpGet]
    public ActionResult<List<Products>> GetAll() => products;

    //Get by Id
    [HttpGet("{id}")]
    public ActionResult<Products> GetById(string id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        return product == null ? NotFound() : Ok(product);
    }

    //Post
    [HttpPost]
    public ActionResult<Products> Create(Products product)
    {
        product.Id = Guid.NewGuid().ToString();
        products.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    //Put
    [HttpPut("{id}")]
    public ActionResult<Products> Update(string id, Products updatedProduct)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;

        return Ok(product);
    }

    //Patch
    [HttpPatch("{id}")]
    public ActionResult<Products> PartialUpdate(string id, JsonPatchDocument<Products> patchDoc)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        patchDoc.ApplyTo(product);
        return Ok(product);
    }

    //Delete    
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        products.Remove(product);
        return NoContent();
    }
}