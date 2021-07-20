using hi_teacher_app_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.repositories.impl
{
    public class CategoriesRepository : ICategoriesRepository
    {

        private readonly HiTeacherDBContext _db;

        public CategoriesRepository(HiTeacherDBContext db)
        {
            _db = db;
        }
        public Category Add(Category entity)
        {
            Category category = _db.Categories.Add(entity).Entity;
            _db.SaveChanges();
            return category;
        }

        public void Delete(Category entity)
        {
            _db.Categories.Remove(entity);
            _db.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Categories;
        }

        public Category GetById(int id)
        {
            return _db.Categories.SingleOrDefault(x => x.CategoryId == id);
        }

        public void Update(Category entity)
        {
            _db.Categories.Update(entity);
            _db.SaveChanges();
        }
    }
}
