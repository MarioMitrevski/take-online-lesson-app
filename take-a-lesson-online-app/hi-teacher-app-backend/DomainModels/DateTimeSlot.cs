
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace hi_teacher_app_backend.Models
{
    public class DateTimeSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DateTimeSlotId { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int CourseGroupId { get; set; }
        public CourseGroup CourseGroup { get; set; }
        public DateTimeSlot() { }

        public DateTimeSlot(string Date, string StartTime, string EndTime)
        {
            this.Date = Date;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }

    }
}
