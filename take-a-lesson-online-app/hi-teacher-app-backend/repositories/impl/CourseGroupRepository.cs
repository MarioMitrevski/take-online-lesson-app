using hi_teacher_app_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.repositories.impl
{
    public class CourseGroupRepository : ICourseGroupRepository
    {
        private readonly HiTeacherDBContext _db;

        public CourseGroupRepository(HiTeacherDBContext db)
        {
            _db = db;
        }

        public CourseGroup Add(CourseGroup entity)
        {
            CourseGroup courseGroup = _db.CourseGroups.Add(entity).Entity;
            _db.SaveChanges();
            return courseGroup;
        }

        public void Delete(CourseGroup entity)
        {
            _db.CourseGroups.Remove(entity);
            _db.SaveChanges();
        }
        public CourseGroup GetById(int id)
        {
            return _db.CourseGroups.SingleOrDefault(x => x.CourseGroupId == id);
        }

        public void Update(CourseGroup entity)
        {
            _db.CourseGroups.Update(entity);
            _db.SaveChanges();
        }

        public IEnumerable<CourseGroup> GetAllByStatus(CourseGroupStatus status)
        {
            if (CourseGroupStatus.UPCOMMING.Value == status.Value)
            {
                return _db.CourseGroups.Where(cg => cg.courseGroupStatus == CourseGroupStatus.UPCOMMING.Value).Include(cg => cg.DateTimeSlots).Include(cg => cg.StudentCourseGroups).ThenInclude(scg => scg.Student).ThenInclude(s => s.User);
            }
            else if (CourseGroupStatus.FINISHED.Value == status.Value)
            {
                return _db.CourseGroups.Where(cg => cg.courseGroupStatus == CourseGroupStatus.FINISHED.Value).Include(cg => cg.DateTimeSlots).Include(cg => cg.StudentCourseGroups).ThenInclude(scg => scg.Student).ThenInclude(s => s.User); ;
            }
            else if (CourseGroupStatus.INPROGRESS.Value == status.Value)
            {
                return _db.CourseGroups.Where(cg => cg.courseGroupStatus == CourseGroupStatus.INPROGRESS.Value).Include(cg => cg.DateTimeSlots).Include(cg => cg.StudentCourseGroups).ThenInclude(scg => scg.Student).ThenInclude(s => s.User); ;
            }
            else
            {
                return _db.CourseGroups.Where(cg => cg.courseGroupStatus == CourseGroupStatus.CANCELED.Value).Include(cg => cg.DateTimeSlots).Include(cg => cg.StudentCourseGroups).ThenInclude(scg => scg.Student).ThenInclude(s => s.User); ;
            }
        }

        public IEnumerable<CourseGroup> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
