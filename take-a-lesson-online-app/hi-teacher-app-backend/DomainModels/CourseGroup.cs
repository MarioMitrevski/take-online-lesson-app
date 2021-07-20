using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace hi_teacher_app_backend.Models
{
    public class CourseGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseGroupId { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string CourseGroupName { get; set; }
        public string CourseGroupGoogleMeetLink { get; set; }
        public int EnrolledStudents { get; set; }
        public int MinStudents { get; set; }
        public int MaxStudents { get; set; }
        public List<DateTimeSlot> DateTimeSlots { get; set; }
        public string courseGroupStatus { get; set; }

        public List<StudentCourseGroups> StudentCourseGroups { get; set; }
        public CourseGroup() { }
        public CourseGroup(string CourseGroupName, string CourseGroupGoogleMeetLink, int MinStudents, int MaxStudents, List<DateTimeSlot> DateTimeSlots)
        {
            this.CourseGroupName = CourseGroupName;
            this.CourseGroupGoogleMeetLink = CourseGroupGoogleMeetLink;
            this.MinStudents = MinStudents;
            this.MaxStudents = MaxStudents;
            this.DateTimeSlots = DateTimeSlots;
            this.courseGroupStatus = CourseGroupStatus.UPCOMMING.Value;

        }

    }


    public class CourseGroupStatus
    {

        private CourseGroupStatus(string value) { Value = value; }

        public string Value { get; set; }

        public static CourseGroupStatus CANCELED { get { return new CourseGroupStatus("COURSE_GROUP_CANCELED"); } }
        public static CourseGroupStatus FINISHED { get { return new CourseGroupStatus("COURSE_GROUP_FINISHED"); } }
        public static CourseGroupStatus UPCOMMING { get { return new CourseGroupStatus("COURSE_GROUP_UPCOMMING"); } }
        public static CourseGroupStatus INPROGRESS { get { return new CourseGroupStatus("COURSE_GROUP_INPROGRESS"); } }
    }

}