using InventoryManagement.Application.DTO.Customer.Request;
using InventoryManagement.Application.DTO.Customer.Response;

namespace InventoryManagement.Application.Interfaces
{
    public interface IUserService : IGenericService<Domain.Models.User, CreateUserDto, UpdateUserDto, ReadUserDto>
    {
        Task<ReadUserDto> UserLogin(UserLoginDto dto);
    }
}

