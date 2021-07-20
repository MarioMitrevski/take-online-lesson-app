using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.Exceptions
{
    public class CourseNotSavedException: Exception
    {
        public HttpStatusCode Status { get; private set; }

        public CourseNotSavedException(HttpStatusCode status = HttpStatusCode.InternalServerError, string msg = "Course Not Saved") : base(msg)
        {
            Status = status;
        }
    }
}
