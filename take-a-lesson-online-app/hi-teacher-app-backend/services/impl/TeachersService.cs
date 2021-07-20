using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.repositories;
using hi_teacher_app_backend.repositories.impl;
using System.Collections.Generic;

namespace hi_teacher_app_backend.services.impl
{
    public class TeachersService : ITeachersService<Teacher>
    {

        private readonly ITeachersRepository _teachersRepository;
        private readonly ICoursesRepository _coursesRepository;


        public TeachersService(ITeachersRepository ITeachersRepository, ICoursesRepository ICoursesRepository)
        {
            _teachersRepository = ITeachersRepository;
            _coursesRepository = ICoursesRepository;
        }
        public void AddTeacher(int UserId, string TeacherDescription)
        {
            _teachersRepository.Add(new Teacher { UserId = UserId, TeacherDescription = TeacherDescription });
        }

        public int? getTeacherByUserID(int UserId)
        {
           return _teachersRepository.GetByUserID(UserId);
        }

        public List<Course> GetUpcommingCourses(int UserId)
        {
            int teacherId = _teachersRepository.GetByUserID(UserId).GetValueOrDefault();
            return _coursesRepository.GetTeacherUpcommingCourses(teacherId);
        }

        public List<Course> GetFinishedCourses(int UserId)
        {
            int teacherId = _teachersRepository.GetByUserID(UserId).GetValueOrDefault();
            return _coursesRepository.GetTeacherFinishedCourses(teacherId);
        }

        public List<Course> GetInProgressCourses(int UserId)
        {
            int teacherId = _teachersRepository.GetByUserID(UserId).GetValueOrDefault();
            return _coursesRepository.GetTeacherInProgressCourses(teacherId);
        }
    }
}
