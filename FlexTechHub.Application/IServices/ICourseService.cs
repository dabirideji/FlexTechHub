using InventoryManagement.Application.DTO.Customer.Request;
using InventoryManagement.Application.DTO.Customer.Response;
using InventoryManagement.Application.Interfaces.GenericRepositoryPattern;

namespace InventoryManagement.Application.Interfaces
{
    public interface ICourseService : IGenericService<Domain.Models.Course, CreateCourseDto, UpdateCourseDto, ReadCourseDto>
    {
    }
}

