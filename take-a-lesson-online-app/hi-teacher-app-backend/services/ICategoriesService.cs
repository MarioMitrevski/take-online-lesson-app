using hi_teacher_app_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.services
{
    public interface ICategoriesService<T>
    {
        IEnumerable<Category> getAll();
        Category getByID(int categoryId);
    }
}
