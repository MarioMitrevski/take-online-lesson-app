using hi_teacher_app_backend.DTOs;
using hi_teacher_app_backend.Exceptions;
using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.services.impl
{
    public class StudentsService : IStudentsService<Student>
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly ICoursesRepository _coursesRepository;


        public StudentsService(IStudentsRepository IStudentsRepository, ICoursesRepository ICoursesRepository)
        {
            _coursesRepository = ICoursesRepository;

            _studentsRepository = IStudentsRepository;
        }
        public void AddStudent(int UserId)
        {
            _studentsRepository.Add(new Student { UserId = UserId });
        }

        public void EnrollCourse(int UserId, int courseGroupId)
        {
            Student student = _studentsRepository.GetByUserID(UserId);
            if(student == null)
            {
                throw new StudentNotFoundException();
            }
            student.StudentCourseGroups.Add(new StudentCourseGroups { CourseGroupId = courseGroupId, StudentId = student.StudentId });
            _studentsRepository.Update(student);
        }

        public List<CourseDTO> GetFinishedCourses(int UserId)
        {
            try
            {
                int studentId = _studentsRepository.GetByUserID(UserId).StudentId;
                return _studentsRepository.GetFinishedCourses(studentId);
            } catch (Exception)
            {
                throw new StudentNotFoundException();
            }
        }

        public List<CourseDTO> GetInProgressCourses(int UserId)
        {
            try
            {
                int studentId = _studentsRepository.GetByUserID(UserId).StudentId;
                return _studentsRepository.GetInProgressCourses(studentId);
            }
            catch (Exception)
            {
                throw new StudentNotFoundException();
            }
        }

        public List<CourseDTO> GetUpcommingCourses(int UserId)
        {
            int studentId = _studentsRepository.GetByUserID(UserId).StudentId;
            return _studentsRepository.GetUpcommingCourses(studentId);
        }
    }
}

