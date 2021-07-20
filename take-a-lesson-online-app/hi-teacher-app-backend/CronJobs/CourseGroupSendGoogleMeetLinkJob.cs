using EmailService;
using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.repositories;
using hi_teacher_app_backend.repositories.impl;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.CronJobs
{
    [DisallowConcurrentExecution]
    public class CourseGroupSendGoogleMeetLinkJob : IJob
    {
        private readonly ILogger<CourseGroupSendGoogleMeetLinkJob> _logger;
        private readonly ICourseGroupRepository _courseGroupRepository;
        private readonly ICoursesRepository _coursesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IEmailSender _emailSender;

        public CourseGroupSendGoogleMeetLinkJob(ILogger<CourseGroupSendGoogleMeetLinkJob> logger, ICourseGroupRepository courseGroupRepository, ICoursesRepository coursesRepository, IUsersRepository usersRepository, IEmailSender emailSender)
        {
            _logger = logger;
            _courseGroupRepository = courseGroupRepository;
            _coursesRepository = coursesRepository;
            _usersRepository = usersRepository;
            _emailSender = emailSender;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");


            var currentDateTimePlusHours = DateTime.Now.AddMinutes(30);
            _courseGroupRepository.GetAllByStatus(CourseGroupStatus.UPCOMMING).ToList().ForEach(cg =>
            {

                var dateTime = Convert.ToDateTime(cg.DateTimeSlots.First().Date + " " + cg.DateTimeSlots.First().StartTime);
                if (dateTime <= currentDateTimePlusHours)
                {
                    cg.courseGroupStatus = CourseGroupStatus.INPROGRESS.Value;
                    
                    _courseGroupRepository.Update(cg);

                    var emails = new List<string>();
                    cg.StudentCourseGroups.ForEach(scg => emails.Add(scg.Student.User.UserName));
                    var course = _coursesRepository.GetCourseWithTeacher(cg.CourseId);
                    emails.Add(course.Teacher.User.UserName);
                    var message = new Message(emails, $"Google Meet link for course: {course.CourseTitle}", $"Here is the Google Meet link for course: {course.CourseTitle}.The course will start in 30 minutes. {cg.CourseGroupGoogleMeetLink} :)");

                    _emailSender.SendEmail(message);
                }
            });

            _coursesRepository.GetAllByStatus(CourseStatus.UPCOMMING).ToList().ForEach(c =>
            {
                if (c.CourseGroups.First(cg => cg.courseGroupStatus.Equals(CourseGroupStatus.INPROGRESS.Value)) != null)
                {
                    c.courseStatus = CourseStatus.INPROGRESS.Value;
                    _coursesRepository.Update(c);

                }
            });
            return Task.CompletedTask;
        }
    }
}