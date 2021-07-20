using System;
using System.Net;

namespace hi_teacher_app_backend.Exceptions
{
    public class FileUploadException : Exception
    {

        public HttpStatusCode Status { get; private set; }

        public FileUploadException(HttpStatusCode status = HttpStatusCode.BadRequest, string msg = "File size can not be empty.") : base(msg)
        {
            Status = status;
        }

    }
}
