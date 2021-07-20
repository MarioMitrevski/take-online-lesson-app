using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }
        public string TeacherDescription { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public List<Course> Courses { get; set; }
    }
}
