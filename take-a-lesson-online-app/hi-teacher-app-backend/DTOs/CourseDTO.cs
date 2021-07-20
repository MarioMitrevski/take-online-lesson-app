using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.DTOs
{
    public class CourseDTO
    {
        public int CourseId;
        public string CourseTitle;
        public int PricePerStudentForSession;
        public int CategoryId;
        public string CourseDescription;
        public string StartDate;

        public CourseDTO() { }
        public CourseDTO(int CourseId,string CourseTitle,int PricePerStudentForSession,int CategoryId,string CourseDescription,string StartDate)
        {
            this.CourseId = CourseId;
            this.CourseTitle = CourseTitle;
            this.PricePerStudentForSession = PricePerStudentForSession;
            this.CategoryId = CategoryId;
            this.CourseDescription = CourseDescription;
            this.StartDate = StartDate;
        }
    }
}
