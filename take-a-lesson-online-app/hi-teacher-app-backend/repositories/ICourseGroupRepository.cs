using hi_teacher_app_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.repositories
{
    public interface ICourseGroupRepository: IRepository<CourseGroup>
    {
        IEnumerable<CourseGroup> GetAllByStatus(CourseGroupStatus status);

    }
}
