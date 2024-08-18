using AutoMapper;
using InventoryManagement.Application.DTO.Customer.Request;
using InventoryManagement.Application.DTO.Customer.Response;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Interfaces.GenericRepositoryPattern;

namespace InventoryManagement.Application.Services.Customer
{
    public class CourseService : GenericService<Domain.Models.Course, CreateCourseDto, UpdateCourseDto, ReadCourseDto>, ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
    }

}
