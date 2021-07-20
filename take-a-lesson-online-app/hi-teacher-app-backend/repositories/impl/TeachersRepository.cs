using hi_teacher_app_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.repositories.impl
{
    public class TeachersRepository : ITeachersRepository
    {
        private readonly HiTeacherDBContext _db;

        public TeachersRepository(HiTeacherDBContext db)
        {
            _db = db;
        }
        public Teacher Add(Teacher entity)
        {
            Teacher teacher = _db.Teachers.Add(entity).Entity;
            _db.SaveChanges();
            return teacher;
        }

        public void Delete(Teacher entity)
        {
            _db.Teachers.Remove(entity);
            _db.SaveChanges();
        }

        public IEnumerable<Teacher> GetAll()
        {
            return _db.Teachers;
        }

        public Teacher GetById(int id)
        {
            return _db.Teachers.SingleOrDefault(x => x.TeacherId == id);
        }

        public int? GetByUserID(int UserId)
        {
            return _db.Teachers.SingleOrDefault(x => x.UserId == UserId)?.TeacherId;
        }

        public void Update(Teacher entity)
        {
            _db.Teachers.Update(entity);
            _db.SaveChanges();
        }

    }
}
