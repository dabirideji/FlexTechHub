namespace InventoryManagement.Application.DTO.Customer.Response
{
  public class ReadCourseDto
  {
    public Guid CourseId { get; set; }
    public String? CourseTitle { get; set; }
    public String? CourseCategory { get; set; }
    public String? CourseSpecialization { get; set; }
    public String? CourseCreatedAt { get; set; }
    public String? CourseUpdatedAt { get; set; }
  }
  public class ReadUserDto
  {
    public Guid UserId { get; set; }
    public String? Role { get; set; }
    public String? UserRole { get; set; }
    public String? UserFirstName { get; set; }
    public String? UserLastName { get; set; }
    public String? UserEmail { get; set; }
    public String? UserPhoneNumber { get; set; }
    public String? UserPassword { get; set; }
    public String? UserCreatedAt { get; set; }
    public String? UserUpdatedAt { get; set; }
  }

  public class ReadTeacherCourseDto
  {
    public Guid TeacherCourseId { get; set; } = Guid.NewGuid();
    public Guid TeacherId { get; set; }
    public Guid CourseId { get; set; }
    public String? CourseName { get; set; }
    public String? Price { get; set; }
    public String? Level { get; set; }
    public String? Rating { get; set; }
    public String? Status { get; set; }
    public String? CourseStartDate { get; set; }
    public String? CourseEndDate { get; set; }
    public String? CourseDuration { get; set; }
    public DateTime? CourseCreatedAt { get; set; } = DateTime.Now;
    public DateTime? CourseUpdatedAt { get; set; } = DateTime.MinValue;
  }

}