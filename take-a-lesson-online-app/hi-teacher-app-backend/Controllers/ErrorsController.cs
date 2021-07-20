using hi_teacher_app_backend.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; 
            var code = 500; 

            if (exception is TeacherNotFoundException teacherNotFoundException) code = (int) teacherNotFoundException.Status;
            if (exception is CourseGroupNotFoundException courseGroupNotFoundException) code = (int)courseGroupNotFoundException.Status;
            if (exception is CategoryNotFoundException categoryNotFoundException) code = (int)categoryNotFoundException.Status;
            if (exception is CourseNotSavedException courseNotSavedException) code = (int)courseNotSavedException.Status;
            if (exception is FileUploadException fileUploadException) code = (int)fileUploadException.Status;
            if (exception is ImageUploadFailedException imageUploadFailedException) code = (int)imageUploadFailedException.Status;
            if (exception is StudentNotFoundException studentNotFoundException) code = (int)studentNotFoundException.Status;

            Response.StatusCode = code;

            return new ErrorResponse(exception); 
        }
    }
}
