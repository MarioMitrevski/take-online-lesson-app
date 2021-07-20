using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hi_teacher_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService<Category> _categoriesService;
        public CategoriesController(ICategoriesService<Category> categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet("all")]
        [EnableCors("Policy1")]
        public IActionResult GetAllCategories()
        {
            return Ok(_categoriesService.getAll());
           
        }
    }
}