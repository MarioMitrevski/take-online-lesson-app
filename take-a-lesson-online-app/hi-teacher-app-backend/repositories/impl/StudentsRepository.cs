using hi_teacher_app_backend.DTOs;
using hi_teacher_app_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.repositories.impl
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly HiTeacherDBContext _db;

        public StudentsRepository(HiTeacherDBContext db)
        {
            _db = db;
        }
        public Student Add(Student entity)
        {
            Student student = _db.Students.Add(entity).Entity;
            _db.SaveChanges();
            return student;
        }

        public void Delete(Student entity)
        {
            _db.Students.Remove(entity);
            _db.SaveChanges();
        }

        public IEnumerable<Student> GetAll()
        {
            return _db.Students;
        }

        public Student GetById(int id)
        {
            return _db.Students.SingleOrDefault(x => x.StudentId == id);
        }

        public Student GetByUserID(int UserId)
        {
            return _db.Students.SingleOrDefault(x => x.UserId == UserId);
        }

        public void Update(Student entity)
        {
            _db.Students.Update(entity);
            _db.SaveChanges();
        }

        public List<CourseDTO> GetUpcommingCourses(int StudentId)
        {
            var student = _db.Students.Where(s => s.StudentId == StudentId).Include(s => s.StudentCourseGroups).ThenInclude(c => c.CourseGroup).ThenInclude(c => c.Course).SingleOrDefault();
            
            List<CourseDTO> courses = new List<CourseDTO>();
            student.StudentCourseGroups
                .Select(x => x.CourseGroup)
                .ToList()
                .ForEach(x =>
                {
                    if (x.courseGroupStatus == CourseGroupStatus.UPCOMMING.Value)
                    {
                        CourseDTO dto = new CourseDTO(x.Course.CourseId, x.Course.CourseTitle, x.Course.PricePerStudentForSession, x.Course.CategoryId, x.Course.CourseDescription, x.Course.StartDate);
                        courses.Add(dto);
                    }

                });

            return courses;
        }

        public List<CourseDTO> GetFinishedCourses(int StudentId)
        {
            var student = _db.Students.Where(s => s.StudentId == StudentId).Include(s => s.StudentCourseGroups).ThenInclude(c => c.CourseGroup).ThenInclude(c => c.Course).SingleOrDefault();
            
            List<CourseDTO> courses = new List<CourseDTO>();
            student.StudentCourseGroups
                .Select(x => x.CourseGroup)
                .ToList()
                .ForEach(x =>
                {
                    if (x.courseGroupStatus == CourseGroupStatus.FINISHED.Value)
                    {
                        CourseDTO dto = new CourseDTO(x.Course.CourseId, x.Course.CourseTitle, x.Course.PricePerStudentForSession, x.Course.CategoryId, x.Course.CourseDescription, x.Course.StartDate);
                        courses.Add(dto);
                    }
                });

            return courses;
        }

        public List<CourseDTO> GetInProgressCourses(int StudentId)
        {
            var student = _db.Students.Where(s => s.StudentId == StudentId).Include(s => s.StudentCourseGroups).ThenInclude(c => c.CourseGroup).ThenInclude(c => c.Course).SingleOrDefault();

            List<CourseDTO> courses = new List<CourseDTO>();
            student.StudentCourseGroups
                .Select(x => x.CourseGroup)
                .ToList()
                .ForEach(x =>
                {
                    if (x.courseGroupStatus == CourseGroupStatus.INPROGRESS.Value)
                    {
                        CourseDTO dto = new CourseDTO(x.Course.CourseId, x.Course.CourseTitle, x.Course.PricePerStudentForSession, x.Course.CategoryId, x.Course.CourseDescription, x.Course.StartDate);
                        courses.Add(dto);
                    }
                });

            return courses;
        }
        
    }
}
