using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hi_teacher_app_backend.Models
{
    public class Student
    {
        public Student()
        {
            StudentCourseGroups = new List<StudentCourseGroups>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<StudentCourseGroups> StudentCourseGroups { get; set; }

    }
}
