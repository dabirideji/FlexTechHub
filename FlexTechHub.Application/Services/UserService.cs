using AutoMapper;
using InventoryManagement.Application.DTO.Customer.Request;
using InventoryManagement.Application.DTO.Customer.Response;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Interfaces.GenericRepositoryPattern;
using InventoryManagement.Domain.Models;

namespace InventoryManagement.Application.Services.Customer
{
    public class UserService : GenericService<User, CreateUserDto, UpdateUserDto, ReadUserDto>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<ReadUserDto> UserLogin(UserLoginDto dto)
        {
            try
            {
                var userRepository = _unitOfWork.GetRepository<User>();
                var users = await userRepository.GetAllAsync();
                var userEmailCheck = users.FirstOrDefault(x => x.UserEmail.ToLower() == dto.UserEmail.ToLower());
                if (userEmailCheck == null)
                {
                    throw new Exception("INVALID USER EMAIL || EMAIL NOT FOUND");
                }

                if (dto.UserPassword != userEmailCheck.UserPassword)
                {
                    throw new UnauthorizedAccessException("INVALID USER PASSWORD || INCORRECT PASSWORD");
                }
                var mappedUser = _mapper.Map<ReadUserDto>(userEmailCheck);
                return mappedUser;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }

}
