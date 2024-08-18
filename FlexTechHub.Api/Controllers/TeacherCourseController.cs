namespace InventoryManagement.Api.Controllers
{
    public class TeacherCourseController : GenericController<TeacherCourse, CreateTeacherCourseDto, UpdateTeacherCourseDto, ReadTeacherCourseDto>
    {
        private readonly ITeacherCourseService _service;

        public TeacherCourseController(ITeacherCourseService service) : base(service)
        {
            this._service = service;
        }

    }

}
