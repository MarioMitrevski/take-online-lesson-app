using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.Pagination;
using hi_teacher_app_backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.repositories
{
    public interface ICoursesRepository: IRepository<Course>
    {
        int GetTotalCourses(int? CategoryId, string search);
        IEnumerable<Course> GetAll(PaginationFilter paginationFilter, int? Categoryid, string search, SortingFilter sortingFilter);

        List<Course> GetTeacherUpcommingCourses(int TeacherId);
        List<Course> GetTeacherFinishedCourses(int TeacherId);
        List<Course> GetTeacherInProgressCourses(int TeacherId);

        IEnumerable<Course> GetAllByStatus(CourseStatus status);
        Course GetCourseWithTeacher(int CourseId);

        Course GetCourseDetails(int CourseId);
    }
}
