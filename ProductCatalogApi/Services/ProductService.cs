using ProductCatalogApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ProductCatalogApi.Services;

public class ProductService : IProductService
{
    private static List<Products> _products = new List<Products>();

    public List<Products> GetAll() => _products;

    public Products? GetById(string id) =>
        _products.FirstOrDefault(p => p.Id == id);

    public Products Create(Products product)
    {
        product.Id = Guid.NewGuid().ToString();
        _products.Add(product);
        return product;
    }

    public Products? Update(string id, Products updatedProduct)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return null;

        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;
        return product;
    }

    public Products? PartialUpdate(string id, JsonPatchDocument<Products> patchDoc)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return null;

        patchDoc.ApplyTo(product);
        return product;
    }

    public bool Delete(string id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return false;

        _products.Remove(product);
        return true;
    }
}
