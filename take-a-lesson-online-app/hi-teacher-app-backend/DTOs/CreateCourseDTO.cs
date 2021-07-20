using hi_teacher_app_backend.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.DTOs
{
    public class CreateCourseDTO
    {
        public int? CourseId { get; set; }

        [Required]
        public string CourseTitle { get; set; }
        [Required]
        public int PricePerStudentForSession { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string CourseDescription { get; set; }

        public List<CreateCourseThemeDTO> CourseThemes { get; set; }
        public List<CreateCourseGroupDTO> CourseGroups { get; set; }
    }
}
