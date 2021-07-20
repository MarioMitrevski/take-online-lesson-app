using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.DTOs
{
    public class CreateCourseGroupDTO
    {
        [Required]
        public string CourseGroupName { get; set; }
        [Required]
        public string CourseGroupGoogleMeetLink { get; set; }
        [Required]
        public int MinStudents { get; set; }
        [Required]
        public int MaxStudents { get; set; }
        public List<CreateCourseGroupDateTimeSlotDTO> DateTimeSlots { get; set; }
    }
}
