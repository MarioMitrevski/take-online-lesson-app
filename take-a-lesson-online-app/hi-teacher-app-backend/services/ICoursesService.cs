using hi_teacher_app_backend.DTOs;
using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.Pagination;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.services
{
    public interface ICoursesService<T>
    {
        Course CreateOrUpdateCourse(CreateCourseDTO courseDTO, int userId);
        PagedResponse<List<Course>> GetAll(CategoriesQuery query);
        string UploadFile(IFormFile ImageFile, int CourseId, string scheme, string host, string pathBase);
        void EnrollCourse(int userId,int CourseGroupId);
        Course GetCourse(int courseId);
    }
}
