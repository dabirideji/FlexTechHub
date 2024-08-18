using AutoMapper;
using InventoryManagement.Application.DTO.Customer.Seller.Request;
using InventoryManagement.Application.DTO.Customer.Seller.Response;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Interfaces.GenericRepositoryPattern;
using InventoryManagement.Domain.Models;

namespace InventoryManagement.Application.Services.Customer
{
    public class SellerService : GenericService<Seller, CreateSellerDto, UpdateSellerDto, ReadSellerDto>, ISellerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SellerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        
        }

        public Task<ReadSellerDto?> AuthenticateAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ForgotPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPasswordAsync(Guid sellerId, string newPassword)
        {
            throw new NotImplementedException();
        }
    }

}
