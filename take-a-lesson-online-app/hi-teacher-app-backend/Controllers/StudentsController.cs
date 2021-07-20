
using System.Security.Claims;
using hi_teacher_app_backend.DomainModels;
using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hi_teacher_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentsService<Student> _studentsService;
        public StudentsController(IStudentsService<Student> studentsService)
        {
            _studentsService = studentsService;
        }

        [HttpGet("courses/upcomming")]
        [Authorize(Policy = Policies.Student)]
        public IActionResult GetStudentUpcommingCourses()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userId = identity.FindFirst("UserId")?.Value;
            var response = _studentsService.GetUpcommingCourses(int.Parse(userId));
            return Ok(response);

        }

        [HttpGet("courses/finished")]
        [Authorize(Policy = Policies.Student)]
        public IActionResult GetStudentFinishedCourses()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userId = identity.FindFirst("UserId")?.Value;
            var response = _studentsService.GetFinishedCourses(int.Parse(userId));
            return Ok(response);

        }

        [HttpGet("courses/inprogress")]
        [Authorize(Policy = Policies.Student)]
        public IActionResult GetStudentInProgressCourses()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userId = identity.FindFirst("UserId")?.Value;
            var response = _studentsService.GetInProgressCourses(int.Parse(userId));
            return Ok(response);

        }

    }
}