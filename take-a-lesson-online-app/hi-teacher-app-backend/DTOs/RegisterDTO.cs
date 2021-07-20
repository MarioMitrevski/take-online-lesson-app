using hi_teacher_app_backend.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace hi_teacher_app_backend.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string BirthDate { get; set; }
        [Required]
        public bool IsTeacher { get; set; }

        [PermittedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile ImageFile { get; set; }
        public string ImageUrl { get; set; }

        public string TeacherDescription { get; set; }


    }
}
