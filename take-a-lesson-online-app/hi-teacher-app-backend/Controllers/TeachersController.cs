using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Apis.Storage.v1.Data;
using hi_teacher_app_backend.DomainModels;
using hi_teacher_app_backend.Exceptions;
using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hi_teacher_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Policy1")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeachersService<Teacher> _teachersService;
        public TeachersController(ITeachersService<Teacher> teachersService)
        {
            _teachersService = teachersService;
        }

        [HttpGet("courses/upcomming")]
        [Authorize(Policy = Policies.Teacher)]
        public IActionResult GetTeacherUpcommingCourses()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            try
            {
                var userId = identity.FindFirst("UserId")?.Value;
                var response = _teachersService.GetUpcommingCourses(int.Parse(userId));
                return Ok(response);
            }
            catch (Exception)
            {
                throw new TeacherNotFoundException();
            }
        }

        [HttpGet("courses/finished")]
        [Authorize(Policy = Policies.Teacher)]
        public IActionResult GetTeacherFinishedCourses()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            try
            {
                var userId = identity.FindFirst("UserId")?.Value;
                var response = _teachersService.GetFinishedCourses(int.Parse(userId));
                return Ok(response);
            }
            catch (Exception)
            {
                throw new TeacherNotFoundException();
            }
        }

        [HttpGet("courses/inprogress")]
        [Authorize(Policy = Policies.Teacher)]
        public IActionResult GetTeacherInProgressCourses()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            try
            {
                var userId = identity.FindFirst("UserId")?.Value;
                var response = _teachersService.GetInProgressCourses(int.Parse(userId));
                return Ok(response);
            }
            catch (Exception)
            {
                throw new TeacherNotFoundException();
            }
        }

    }
}