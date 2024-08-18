using InventoryManagement.Application.DTO.Product.Request;
using InventoryManagement.Application.DTO.Product.Response;
using InventoryManagement.Domain.Models;

namespace InventoryManagement.Application.Interfaces
{
    public interface IProductService : IGenericService<Product, CreateProductDto, UpdateProductDto, ReadProductDto>
    {
        Task<bool> ProductExistsAsync(string name);
    }
}

