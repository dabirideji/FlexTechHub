using System.ComponentModel.DataAnnotations;
using InventoryManagement.Domain.Models.Categories;

namespace InventoryManagement.Application.DTO.Customer.Request
{
  public class CreateCourseDto
  {
    public String? CourseTitle { get; set; }
    public String? CourseCategory { get; set; }
    public String? CourseSpecialization { get; set; }
  }
  public class CreateUserDto
  {
    public String? Role { get; set; }
    public String? UserFirstName { get; set; }
    public String? UserLastName { get; set; }
    public String? UserEmail { get; set; }
    public String? UserPhoneNumber { get; set; }
    public String? UserPassword { get; set; }
    [Compare("UserPassword")]
    public String? UserConfirmPassword { get; set; }
  }
  public class UpdateUserDto
  {
    public String? UserFirstName { get; set; }
    public String? UserLastName { get; set; }
    public String? UserEmail { get; set; }
    public String? UserPhoneNumber { get; set; }
    public String? UserPassword { get; set; }
    [Compare("UserPassword")]
    public String? UserConfirmPassword { get; set; }
  }




  public class CreateTeacherCourseDto
  {
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
  }
  public class CreateStudentCourseDto
  {
    public String? CourseName { get; set; }
    public String? Price { get; set; }
    public String? Level { get; set; }
    public String? Rating { get; set; }
    public String? Status { get; set; }
    public String? CourseStartDate { get; set; }
    public String? CourseEndDate { get; set; }
    public String? CourseDuration { get; set; }
  }

  public class UpdateStudentCourseDto
  {
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
    public DateTime? CourseUpdatedAt { get; set; } = DateTime.Now;
  }
  public class UpdateTeacherCourseDto
  {
    public String? CourseName { get; set; }
    public String? Price { get; set; }
    public String? Level { get; set; }
    public String? Rating { get; set; }
    public String? Status { get; set; }
    public String? CourseStartDate { get; set; }
    public String? CourseEndDate { get; set; }
    public String? CourseDuration { get; set; }
  }

  public class UserLoginDto
  {
    public String? UserEmail { get; set; }
    public String? UserPassword { get; set; }
  }






}