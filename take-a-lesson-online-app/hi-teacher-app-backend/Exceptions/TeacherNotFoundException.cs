using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.Exceptions
{
    public class TeacherNotFoundException : Exception
    {
        public HttpStatusCode Status { get; private set; }

        public TeacherNotFoundException(HttpStatusCode status = HttpStatusCode.NotFound, string msg = "Teacher Not Found") : base(msg)
        {
            Status = status;
        }
    }
}
