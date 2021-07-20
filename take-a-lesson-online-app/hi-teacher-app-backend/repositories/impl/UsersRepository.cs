using hi_teacher_app_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.repositories.impl
{
    public class UsersRepository : IUsersRepository
    {

        private readonly HiTeacherDBContext _db;

        public UsersRepository(HiTeacherDBContext db)
        {
            _db = db;
        }
        public User Add(User entity)
        {
            User user = _db.Users.Add(entity).Entity;
            _db.SaveChanges();
            return user;
        }

        public void Delete(User entity)
        {
            _db.Users.Remove(entity);
            _db.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public User GetById(int id)
        {
            return _db.Users.SingleOrDefault(x => x.UserId == id);
        }

        public void Update(User entity)
        {
            _db.Users.Update(entity);
            _db.SaveChanges();
        }
    }
}
