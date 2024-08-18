using InventoryManagement.Application.DTO.Customer.Request;
using InventoryManagement.Application.DTO.Customer.Response;

namespace InventoryManagement.Application.Interfaces
{
    public interface ITeacherCourseService : IGenericService<Domain.Models.TeacherCourse, CreateTeacherCourseDto, UpdateTeacherCourseDto, ReadTeacherCourseDto>
    {
    }
}

