namespace InventoryManagement.Api.Controllers
{

    // public class CourseController : GenericController<Course, CreateCourseDto, UpdateCourseDto, ReadCourseDto>
    // {
    //     public CourseController(IGenericService<Course, CreateCourseDto, UpdateCourseDto, ReadCourseDto> service) : base(service)
    //     {
    //     }



    // }


    public class CourseController : GenericController<Course, CreateCourseDto, UpdateCourseDto, ReadCourseDto>
    {
        private readonly ICourseService _service;

        public CourseController(ICourseService service) : base(service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult> GetCoursesByCategory(string category){
            var courses=await _service.GetAllAsync();
            var filter=courses.Where(x=>x.CourseCategory.ToLower()==category.ToLower());
            if(filter==null){
                return NotFound();
            }
            return Ok(filter);
        }


        [HttpGet]
        public async Task<ActionResult> SearchCourse(string query)
        {
            var courses = await _service.GetAllAsync();
            var filter = courses.Where(x => x.CourseCategory.ToLower().Contains(query.ToLower())
                                    || x.CourseTitle.ToLower().Contains(query.ToLower())
                                    || x.CourseSpecialization.ToLower().Contains(query.ToLower()) 
                                    || x.CourseId.ToString().ToLower().Contains(query.ToLower()));
            if (filter == null)
            {
                return NotFound();
            }
            return Ok(filter);
        }


    }

}
