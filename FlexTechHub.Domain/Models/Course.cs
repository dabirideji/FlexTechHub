using System.ComponentModel.DataAnnotations;
using InventoryManagement.Domain.Models.Categories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Domain.Models
{
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; } = Guid.NewGuid();
        public String? CourseTitle { get; set; }
        public String? CourseCategory { get; set; }
        public String? CourseSpecialization { get; set; }
        public String? CourseCreatedAt { get; set; }
        public String? CourseUpdatedAt { get; set; }
    }
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public String? Role { get; set; } = "USER";
        public String? UserFirstName { get; set; }
        public String? UserLastName { get; set; }
        public String? UserEmail { get; set; }
        public String? UserPhoneNumber { get; set; }
        public String? UserPassword { get; set; }
        public String? UserCreatedAt { get; set; }
        public String? UserUpdatedAt { get; set; }
    }
    public class Student : User
    {

        public String? Role { get; set; } = "STUDENT";

    }
    public class Teacher : User
    {

        public String? Role { get; set; } = "TEACHER";
        public String? CV { get; set; }
        public String? Resume { get; set; }
        public String? Rating { get; set; }
        public String? Degree { get; set; }
        public String? CoursesOffered { get; set; }

    }

    [PrimaryKey("StudentId", "TeacherCourseId")]
    public class StudentCourse
    {
        public Guid StudentId { get; set; }
        public Guid TeacherCourseId { get; set; }
        public String? Status { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.MinValue;
    }

    public class TeacherCourse
    {
        [Key]
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


    //    public class Customer
    //     {
    //         public Guid CustomerId { get; set; }=Guid.NewGuid();
    //         public String? CustomerEmail { get; set; }
    //         public String? CustomerPassword { get; set; }
    //         public CustomerType CustomerType { get; set; }=CustomerType.CORE;
    //         public DateTime CustomerCreatedAt { get; set; }=DateTime.Now;
    //         public DateTime CustomerUpdatedAt { get; set; }=DateTime.MinValue;
    //     }



}