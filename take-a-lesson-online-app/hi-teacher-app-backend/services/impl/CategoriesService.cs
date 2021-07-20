using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.services.impl
{
    public class CategoriesService : ICategoriesService<Category>
    {
        private readonly ICategoriesRepository _categoriesRepository;


        public CategoriesService(ICategoriesRepository ICategoriesRepository)
        {
            _categoriesRepository = ICategoriesRepository;
        }

        public IEnumerable<Category> getAll()
        {
            return _categoriesRepository.GetAll().ToList();
        }

        public Category getByID(int categoryId)
        {
            return _categoriesRepository.GetById(categoryId);
        }
    }
}
