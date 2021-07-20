using hi_teacher_app_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.repositories.impl
{
    public interface ITeachersRepository : IRepository<Teacher>
    {
        int? GetByUserID(int UserId);

    }
}
