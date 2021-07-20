using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.Exceptions
{
    public class CategoryNotFoundException: Exception
    {
        public HttpStatusCode Status { get; private set; }

        public CategoryNotFoundException(HttpStatusCode status = HttpStatusCode.NotFound, string msg = "Category Not Found") : base(msg)
        {
            Status = status;
        }
    }
}
