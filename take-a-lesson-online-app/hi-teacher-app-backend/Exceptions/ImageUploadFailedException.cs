using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.Exceptions
{
    public class ImageUploadFailedException: Exception
    {
        public HttpStatusCode Status { get; private set; }

        public ImageUploadFailedException(HttpStatusCode status = HttpStatusCode.InternalServerError, string msg = "Image Upload Failed") : base(msg)
        {
            Status = status;
        }
    }
}
