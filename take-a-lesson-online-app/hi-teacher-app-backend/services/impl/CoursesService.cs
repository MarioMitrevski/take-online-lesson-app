using hi_teacher_app_backend.DTOs;
using hi_teacher_app_backend.Exceptions;
using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.Pagination;
using hi_teacher_app_backend.repositories;
using hi_teacher_app_backend.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading;

namespace hi_teacher_app_backend.services.impl
{
    public class CoursesService : ICoursesService<Course>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ICourseGroupRepository _courseGroupsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStudentsService<Student> _studentsService;

        private readonly ITeachersService<Teacher> _teachersService;
        private readonly ICategoriesService<Category> _categoriesService;
        private static Mutex mutex;

        public CoursesService(ICoursesRepository ICoursesRepository, ITeachersService<Teacher> ITeachersService, IStudentsService<Student> IStudentsService, ICategoriesService<Category> CategoriesService, ICourseGroupRepository ICourseGroupsRepository, IWebHostEnvironment webHostEnvironment)
        {
            _coursesRepository = ICoursesRepository;
            _courseGroupsRepository = ICourseGroupsRepository;
            _webHostEnvironment = webHostEnvironment;
            _studentsService = IStudentsService;

            _teachersService = ITeachersService;
            _categoriesService = CategoriesService;
            mutex = new Mutex();
        }
        public Course CreateOrUpdateCourse(CreateCourseDTO courseDTO, int userId)
        {
            try
            {
                var teacherId = _teachersService.getTeacherByUserID(userId);
                if (teacherId == null)
                {
                    throw new CourseNotSavedException();
                }

                Category category = _categoriesService.getByID(courseDTO.CategoryId);
                if (category == null)
                {
                    throw new CourseNotSavedException();
                }

                List<CourseTheme> courseThemes = new List<CourseTheme>();
                courseDTO.CourseThemes.ForEach(t => courseThemes.Add(new CourseTheme(t.CourseThemeName, t.CourseThemeDescription)));

                List<CourseGroup> courseGroups = new List<CourseGroup>();

                var startDate = new DateTime(3000, 1, 1, 1, 1, 1);
                courseDTO.CourseGroups.ForEach(c =>
                {
                    List<DateTimeSlot> dateTimeSlots = new List<DateTimeSlot>();

                    c.DateTimeSlots.ForEach(d =>
                    {
                        dateTimeSlots.Add(new DateTimeSlot(d.Date, d.StartTime, d.EndTime));
                        var currDate = DateTime.Parse(d.Date);

                        if (startDate.CompareTo(currDate) > 0)
                        {
                            startDate = currDate;
                        }

                        });
                    courseGroups.Add(new CourseGroup(c.CourseGroupName, c.CourseGroupGoogleMeetLink, c.MinStudents, c.MaxStudents, dateTimeSlots));
                });

                Course course = new Course(courseDTO.CourseTitle, courseDTO.PricePerStudentForSession, category.CategoryId, (int)teacherId, courseDTO.CourseDescription, courseThemes, courseGroups, startDate.ToString("dd/MM/yyyy"));

                if (courseDTO.CourseId != null)
                {
                    course.CourseId = (int)courseDTO.CourseId;
                    _coursesRepository.Update(course);
                    return course;
                }
                else
                {
                    return _coursesRepository.Add(course);
                }

            }
            catch (Exception ex)
            {
                throw new CourseNotSavedException();
            }
        }

        public PagedResponse<List<Course>> GetAll(CategoriesQuery query)
        {
            var validPagingFilter = new PaginationFilter(query.PageNumber, query.PageSize);
            var validSortingFilter = new SortingFilter(query.SortBy, query.OrderBy);

            var pagedData = _coursesRepository.GetAll(validPagingFilter, query.CategoryId, query.Search?.ToLower(), validSortingFilter).ToList();
            var totalNumber = _coursesRepository.GetTotalCourses(query.CategoryId, query.Search?.ToLower());

            return new PagedResponse<List<Course>>(pagedData, validPagingFilter.PageNumber, pagedData.Count, (int)Math.Ceiling(totalNumber / (double)validPagingFilter.PageSize), totalNumber);
        }

        public string UploadFile(IFormFile ImageFile, int CourseId, string scheme, string host, string pathBase)
        {
            try
            { 

                if (ImageFile.Length > 0)
                {
                    Course course = _coursesRepository.GetById(CourseId);
                    string fileName = FormFileName(CourseId);

                    var pathToSave = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", fileName);
                    using (var stream = new FileStream(pathToSave, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }
                    course.ImageUrl = String.Format("{0}://{1}{2}/Images/{3}", scheme, host, pathBase, fileName);
                    _coursesRepository.Update(course);
                    return fileName;
                }
                else
                {
                    throw new FileUploadException();
                }
            }
            catch (Exception)
            {
                throw new ImageUploadFailedException();
            }
        }

        private static string FormFileName(int CourseId)
        {
            var fileNameForStorage = $"{CourseId}.png";
            return fileNameForStorage;
        }

        public void EnrollCourse(int userId, int CourseGroupId)
        {
           
            CourseGroup courseGroup = _courseGroupsRepository.GetById(CourseGroupId);
            if(courseGroup == null)
            {
                throw new CourseGroupNotFoundException();
            }
            mutex.WaitOne();
            if (courseGroup.EnrolledStudents < courseGroup.MaxStudents)
            {
                courseGroup.EnrolledStudents++;
            }
            _studentsService.EnrollCourse(userId, CourseGroupId);
            _courseGroupsRepository.Update(courseGroup);
            mutex.ReleaseMutex();
        }

        public Course GetCourse(int courseId)
        {
            return _coursesRepository.GetCourseDetails(courseId);
         
        }
    }
}
