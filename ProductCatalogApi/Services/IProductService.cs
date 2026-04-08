using ProductCatalogApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ProductCatalogApi.Services;

public interface IProductService
{
    List<Products> GetAll();
    Products? GetById(string id);
    Products Create(Products product);
    Products? Update(string id, Products updatedProduct);
    Products? PartialUpdate(string id, JsonPatchDocument<Products> patchDoc);
    bool Delete(string id);
}
