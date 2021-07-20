using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.Pagination;
using hi_teacher_app_backend.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hi_teacher_app_backend.repositories.impl
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly HiTeacherDBContext _db;

        public CoursesRepository(HiTeacherDBContext db)
        {
            _db = db;
        }

        public Course Add(Course entity)
        {
            Course course = _db.Courses.Add(entity).Entity;
            _db.SaveChanges();
            return course;
        }

        public void Delete(Course entity)
        {
            _db.Courses.Remove(entity);
            _db.SaveChanges();
        }

        public int GetTotalCourses(int? CategoryId, string Search)
        {
            if (CategoryId != null)
            {
                if (Search != null)
                {
                    return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value & c.CategoryId == CategoryId && (c.CourseTitle.ToLower().Contains(Search) || c.CourseDescription.ToLower().Contains(Search))).Count();
                }
                else
                {
                    return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value & c.CategoryId == CategoryId).Count();
                }
            }
            else
            {
                if (Search != null)
                {
                    return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && (c.CourseTitle.ToLower().Contains(Search) || c.CourseDescription.ToLower().Contains(Search))).Count();
                }
                else
                {
                    return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value).Count();
                }

            }
        }

        public Course GetById(int id)
        {
            return _db.Courses.SingleOrDefault(x => x.CourseId == id);
        }

        public Course GetCourseDetails(int id)
        {
            return _db.Courses.Where(x => x.CourseId == id).Include(x=>x.Teacher).ThenInclude(x=>x.User).Include(x => x.Category).Include(x => x.CourseGroups).ThenInclude(x => x.DateTimeSlots)
                .Include(x => x.CourseThemes).FirstOrDefault();
                
        }

        public void Update(Course entity)
        {
            _db.Courses.Update(entity);
            _db.SaveChanges();
        }

        public IEnumerable<Course> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetAll(PaginationFilter filter, int? CategoryId, string search, SortingFilter sortingFilter)
        {
            if (CategoryId != null)
            {
                if (search != null)
                {
                    if (sortingFilter.IsPriceAscending())
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && c.CategoryId == CategoryId && (c.CourseTitle.ToLower().Contains(search) || c.CourseDescription.ToLower().Contains(search))).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderBy(c => c.PricePerStudentForSession).ToList();
                    else if (sortingFilter.IsPriceDescending())
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && c.CategoryId == CategoryId && (c.CourseTitle.ToLower().Contains(search) || c.CourseDescription.ToLower().Contains(search))).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderByDescending(c => c.PricePerStudentForSession).ToList();
                    else if (sortingFilter.IsDateAscending())
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && c.CategoryId == CategoryId && (c.CourseTitle.ToLower().Contains(search) || c.CourseDescription.ToLower().Contains(search))).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderBy(c => c.StartDate).ToList();
                    else
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && c.CategoryId == CategoryId && (c.CourseTitle.ToLower().Contains(search) || c.CourseDescription.ToLower().Contains(search))).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderByDescending(c => c.StartDate).ToList();
                }
                else
                {
                    if (sortingFilter.IsPriceAscending())
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && c.CategoryId == CategoryId).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderBy(c => c.PricePerStudentForSession).ToList();
                    else if (sortingFilter.IsPriceDescending())
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && c.CategoryId == CategoryId).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderByDescending(c => c.PricePerStudentForSession).ToList();
                    else if (sortingFilter.IsDateAscending())
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && c.CategoryId == CategoryId).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderBy(c => c.StartDate).ToList();
                    else
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && c.CategoryId == CategoryId).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderByDescending(c => c.StartDate).ToList();

                }
            }
            else
            {
                if (search != null)
                {
                    if (sortingFilter.IsPriceAscending())
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && (c.CourseTitle.ToLower().Contains(search) || c.CourseDescription.ToLower().Contains(search))).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderBy(c => c.PricePerStudentForSession).ToList();
                    else if (sortingFilter.IsPriceDescending())
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && (c.CourseTitle.ToLower().Contains(search) || c.CourseDescription.ToLower().Contains(search))).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderByDescending(c => c.PricePerStudentForSession).ToList();
                    else if (sortingFilter.IsDateAscending())
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && (c.CourseTitle.ToLower().Contains(search) || c.CourseDescription.ToLower().Contains(search))).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderBy(c => c.StartDate).ToList();
                    else
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && (c.CourseTitle.ToLower().Contains(search) || c.CourseDescription.ToLower().Contains(search))).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderByDescending(c => c.StartDate).ToList();
                }
                else
                {
                    if (sortingFilter.IsPriceAscending())
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderBy(c => c.PricePerStudentForSession).ToList();
                    else if (sortingFilter.IsPriceDescending())
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderByDescending(c => c.PricePerStudentForSession).ToList();
                    else if (sortingFilter.IsDateAscending())
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderBy(c => c.StartDate).ToList();
                    else
                        return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).OrderByDescending(c => c.StartDate).ToList();
                }

            }

        }

        public List<Course> GetTeacherUpcommingCourses(int TeacherId)
        {
            return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value && c.TeacherId == TeacherId).OrderBy(c => c.StartDate).ToList();
        }

        public List<Course> GetTeacherFinishedCourses(int TeacherId)
        {
            return _db.Courses.Where(c => c.courseStatus == CourseStatus.FINISHED.Value && c.TeacherId == TeacherId).OrderBy(c => c.StartDate).ToList();
        }

        public List<Course> GetTeacherInProgressCourses(int TeacherId)
        {
            return _db.Courses.Where(c => c.courseStatus == CourseStatus.INPROGRESS.Value && c.TeacherId == TeacherId).OrderBy(c => c.StartDate).ToList();
        }

        public IEnumerable<Course> GetAllByStatus(CourseStatus status)
        {
            if (CourseStatus.UPCOMMING.Value == status.Value)
            {
                return _db.Courses.Where(c => c.courseStatus == CourseStatus.UPCOMMING.Value).Include(c => c.CourseGroups);
            }
            else if (CourseStatus.FINISHED.Value == status.Value)
            {
                return _db.Courses.Where(c => c.courseStatus == CourseStatus.FINISHED.Value).Include(c => c.CourseGroups);
            }
            else if (CourseStatus.INPROGRESS.Value == status.Value)
            {
                return _db.Courses.Where(c => c.courseStatus == CourseStatus.INPROGRESS.Value).Include(c => c.CourseGroups);
            }
            else
            {
                return _db.Courses.Where(c => c.courseStatus == CourseStatus.CANCELED.Value).Include(c => c.CourseGroups);
            }
        }

        public Course GetCourseWithTeacher(int CourseId)
        {
            return _db.Courses.Where(c => c.CourseId == CourseId).Include(c => c.Teacher).ThenInclude(cs => cs.User).First();
        }
    }
}
