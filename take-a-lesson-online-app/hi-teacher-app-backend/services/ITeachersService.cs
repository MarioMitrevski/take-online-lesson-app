using hi_teacher_app_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.services
{
    public interface ITeachersService<T>
    {
        void AddTeacher(int UserId, string TeacherDescription);
        int? getTeacherByUserID(int UserId);

        List<Course> GetUpcommingCourses(int UserId);
        List<Course> GetFinishedCourses(int UserId);
        List<Course> GetInProgressCourses(int UserId);

    }
    
}
