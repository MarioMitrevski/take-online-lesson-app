using hi_teacher_app_backend.DTOs;
using hi_teacher_app_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.services
{
    public interface IStudentsService<T>
    {
        void AddStudent(int UserId);
        List<CourseDTO> GetUpcommingCourses(int UserId);
        List<CourseDTO> GetFinishedCourses(int UserId);
        List<CourseDTO> GetInProgressCourses(int UserId);

        void EnrollCourse(int UserId, int courseGroupId);

    }
}
