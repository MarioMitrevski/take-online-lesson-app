
using hi_teacher_app_backend.DTOs;
using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.Responses;
using hi_teacher_app_backend.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUsersService<User> _usersService;
        public UsersController(IUsersService<User> usersService)
        {
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [EnableCors("Policy1")]
        public IActionResult Authenticate([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var user = _usersService.Authenticate(loginDTO.UserName, loginDTO.Password);

                if (user == null)
                {
                    return NotFound("Username or Password is incorrect!");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [EnableCors("Policy1")]
        public IActionResult Register([FromForm] RegisterDTO user)
        {
            try
            {
                _usersService.Register(user,Request.Scheme, Request.Host.Value, Request.PathBase);
                var confirmationResponse = new ConfirmationResponse();
                return Ok(confirmationResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}