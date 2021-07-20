
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hi_teacher_app_backend.Models
{
    public class Course
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int PricePerStudentForSession { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public string CourseDescription { get; set; }

        public string StartDate { get; set; }
        public List<CourseTheme> CourseThemes { get; set; }
        public List<CourseGroup> CourseGroups { get; set; }
        public string courseStatus { get; set; }

        public string ImageUrl { get; set; }
        public Course() { }
        public Course(string CourseTitle, int PricePerStudentForSession, int CategoryId, int TeacherId, string CourseDescription, List<CourseTheme> CourseThemes, List<CourseGroup> CourseGroups, string StartDate)
        {
            this.CourseTitle = CourseTitle;
            this.CategoryId = CategoryId;
            this.TeacherId = TeacherId;
            this.CourseDescription = CourseDescription;
            this.CourseThemes = CourseThemes;
            this.CourseGroups = CourseGroups;
            this.PricePerStudentForSession = PricePerStudentForSession;
            this.StartDate = StartDate;
            this.courseStatus = CourseStatus.UPCOMMING.Value;
        }

    }
    public class CourseStatus
    {
        private CourseStatus(string value) { Value = value; }

        public string Value { get; set; }

        public static CourseStatus CANCELED { get { return new CourseStatus("CANCELED"); } }
        public static CourseStatus FINISHED { get { return new CourseStatus("FINISHED"); } }
        public static CourseStatus UPCOMMING { get { return new CourseStatus("UPCOMMING"); } }
        public static CourseStatus INPROGRESS { get { return new CourseStatus("INPROGRESS"); } }

    }
    
}
