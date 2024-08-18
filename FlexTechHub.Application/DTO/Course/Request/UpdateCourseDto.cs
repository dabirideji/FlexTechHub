namespace InventoryManagement.Application.DTO.Customer.Request
{
  public class UpdateCourseDto
  {
        public Guid? CourseId { get; set; }
        public String? CourseTitle { get; set; }
        public String? CourseCategory { get; set; }
        public String? CourseSpecialization { get; set; }
  }
}