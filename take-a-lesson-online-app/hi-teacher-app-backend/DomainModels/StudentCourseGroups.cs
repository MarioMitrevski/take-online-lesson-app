
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace hi_teacher_app_backend.Models
{
    public class StudentCourseGroups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseGroupId { get; set; }
        public CourseGroup CourseGroup { get; set; }

    }
}
