
using hi_teacher_app_backend.DomainModels;
using hi_teacher_app_backend.DTOs;
using hi_teacher_app_backend.Exceptions;
using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.Pagination;
using hi_teacher_app_backend.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hi_teacher_app_backend.Responses;
using System;
using System.Security.Claims;

namespace hi_teacher_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Policy1")]
    public class CoursesController : ControllerBase
    {

        private readonly ICoursesService<Course> _coursesService;
        public CoursesController(ICoursesService<Course> coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpPost("create")]
        [Authorize(Policy = Policies.Teacher)]
        public IActionResult CreateCourse([FromBody] CreateCourseDTO createCourseDTO)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userId = identity.FindFirst("UserId")?.Value;
            Course course =  _coursesService.CreateOrUpdateCourse(createCourseDTO, int.Parse(userId));
            var courseResponse = new CourseResponse(course.CourseId, course.CourseTitle);
            return Ok(courseResponse);

        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] CategoriesQuery query)
        {
            var response = _coursesService.GetAll(query);
            return Ok(response);
        }

        [HttpGet("course")]
        public IActionResult GetCourse([FromQuery] int CourseId)
        {
            var response = _coursesService.GetCourse(CourseId);
            return Ok(response);
        }

        [HttpPost("uploadFile")]
        [Authorize(Policy = Policies.Teacher)]
        public IActionResult UploadFile([FromForm] [PermittedExtensions(new string[] { ".jpg", ".jpeg", ".png" })] IFormFile ImageFile, [FromForm] int CourseId)
        {
            _coursesService.UploadFile(ImageFile, CourseId, Request.Scheme, Request.Host.Value, Request.PathBase);
            return Ok();
        }

        [HttpPost("enroll")]
        [Authorize(Policy = Policies.Student)]
        public IActionResult EnrollCourse([FromQuery] int CourseGroupId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userId = identity.FindFirst("UserId")?.Value;

            _coursesService.EnrollCourse(int.Parse(userId), CourseGroupId);
            return Ok();
        }



    }
}
