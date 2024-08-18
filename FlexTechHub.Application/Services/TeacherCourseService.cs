using AutoMapper;
using InventoryManagement.Application.DTO.Customer.Request;
using InventoryManagement.Application.DTO.Customer.Response;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Interfaces.GenericRepositoryPattern;
using InventoryManagement.Domain.Models;

namespace InventoryManagement.Application.Services.Customer
{
    public class TeacherCourseService : GenericService<TeacherCourse, CreateTeacherCourseDto, UpdateTeacherCourseDto, ReadTeacherCourseDto>, ITeacherCourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeacherCourseService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }


        public override async Task AddAsync(CreateTeacherCourseDto createDto)
        {
            var _userRepository = _unitOfWork.GetRepository<User>();
            var _courseRepository = _unitOfWork.GetRepository<Course>();
            var _teacherCourseRepository = _unitOfWork.GetRepository<TeacherCourse>();

            var validCourse = await _courseRepository.GetByIdAsync(createDto.CourseId);
            if (validCourse is null)
            {
                throw new InvalidOperationException("INVALID COURSE ID ||  COURSE DOES NOT EXIST");
            }

            var validTeacher = await _userRepository.GetByIdAsync(createDto.TeacherId);

            if (validTeacher is null || validTeacher?.Role?.ToLower() != "teacher")
            {
                throw new InvalidOperationException("INVALID TEACHER ID ||  TEACHER DOES NOT EXIST");
            }
            var allCourses = await _teacherCourseRepository.GetAllAsync();
            var courseAlreadyAExists = allCourses.Any(x => x.TeacherId == createDto.TeacherId && x.CourseId == createDto.CourseId);
            if (courseAlreadyAExists)
            {
                throw new InvalidOperationException("COURSE ALREADY EXISTS");
            }

            await base.AddAsync(createDto);

        }
    }

}
