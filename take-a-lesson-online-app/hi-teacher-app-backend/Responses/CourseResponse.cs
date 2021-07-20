using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.Responses
{
    public class CourseResponse
    {
        public int CourseId;

        public string CourseName;

        public CourseResponse(int courseId, string courseName)
        {
            CourseId = courseId;
            CourseName = courseName;
        }
    }
}
