using hi_teacher_app_backend.DTOs;
using hi_teacher_app_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.repositories
{
    public interface IStudentsRepository: IRepository<Student>
    {
        Student GetByUserID(int UserId);
        List<CourseDTO> GetUpcommingCourses(int StudentId);
        List<CourseDTO> GetFinishedCourses(int StudentId);
        List<CourseDTO> GetInProgressCourses(int StudentId);

        
    }
}
