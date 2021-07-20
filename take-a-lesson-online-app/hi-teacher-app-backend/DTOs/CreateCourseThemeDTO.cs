using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.DTOs
{
    public class CreateCourseThemeDTO
    {
        [Required]
        public string CourseThemeName { get; set; }
        [Required]
        public string CourseThemeDescription { get; set; }
    }
}
