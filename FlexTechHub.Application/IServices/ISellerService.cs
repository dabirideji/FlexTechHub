using InventoryManagement.Application.DTO.Customer.Seller.Request;
using InventoryManagement.Application.DTO.Customer.Seller.Response;
using InventoryManagement.Domain.Models;

namespace InventoryManagement.Application.Interfaces
{
    public interface ISellerService : IGenericService<Seller, CreateSellerDto, UpdateSellerDto, ReadSellerDto>
    {
        Task<ReadSellerDto?> AuthenticateAsync(string email, string password);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(Guid sellerId, string newPassword);
    }
}

