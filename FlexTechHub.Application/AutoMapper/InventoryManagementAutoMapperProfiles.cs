using AutoMapper;
using InventoryManagement.Application.DTO.Customer.Request;
using InventoryManagement.Application.DTO.Customer.Response;
using InventoryManagement.Application.DTO.Customer.Seller.Request;
using InventoryManagement.Application.DTO.Customer.Seller.Response;
using InventoryManagement.Application.DTO.Product.Request;
using InventoryManagement.Application.DTO.Product.Response;
using InventoryManagement.Domain.Models;

namespace InventoryManagement.Application.Utilities.AutoMapper
{
    public class InventoryManagementAutoMapperProfiles : Profile
    {
        public InventoryManagementAutoMapperProfiles()
        {
            //COURSE
            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();
            CreateMap<ReadCourseDto, Course>().ReverseMap();


            //TEACHER COURSE
            CreateMap<CreateTeacherCourseDto, TeacherCourse>();
            CreateMap<UpdateTeacherCourseDto, TeacherCourse>();
            CreateMap<ReadTeacherCourseDto, TeacherCourse>().ReverseMap();


            //USER
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<ReadUserDto, User>().ReverseMap();


            //PRODUCT
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<ReadProductDto, Product>().ReverseMap();

        }
    }
}