
using EmailService;
using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.repositories;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.CronJobs
{
    [DisallowConcurrentExecution]
    public class CourseGroupMinParticipantsJob : IJob
    {
        private readonly ILogger<CourseGroupMinParticipantsJob> _logger;
        private readonly ICourseGroupRepository _courseGroupRepository;
        private readonly ICoursesRepository _coursesRepository;
        private readonly IEmailSender _emailSender;

        public CourseGroupMinParticipantsJob(ILogger<CourseGroupMinParticipantsJob> logger, ICourseGroupRepository courseGroupRepository, ICoursesRepository coursesRepository, IEmailSender emailSender)
        {
            _logger = logger;
            _courseGroupRepository = courseGroupRepository;
            _coursesRepository = coursesRepository;
            _emailSender = emailSender;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");


            var currentDateTimePlusHours = DateTime.Now.AddHours(4);
            _courseGroupRepository.GetAllByStatus(CourseGroupStatus.UPCOMMING).ToList().ForEach(cg =>
            {

                var dateTime = Convert.ToDateTime(cg.DateTimeSlots.First().Date + " " + cg.DateTimeSlots.First().StartTime);
                if (dateTime <= currentDateTimePlusHours && cg.EnrolledStudents < cg.MinStudents)
                {
                    cg.courseGroupStatus = CourseGroupStatus.CANCELED.Value;
                    _courseGroupRepository.Update(cg);

                    var emails = new List<string>();
                    cg.StudentCourseGroups.ForEach(scg => emails.Add(scg.Student.User.UserName));
                    var courseTitle = _coursesRepository.GetById(cg.CourseId).CourseTitle;
                    var message = new Message(emails, $"Course: {courseTitle} Canceled", $"Minimum required students for course: {courseTitle} was not achieved. The course is canceled. Your money will be refunded.");

                    _emailSender.SendEmail(message);
                }
            });

            _coursesRepository.GetAllByStatus(CourseStatus.UPCOMMING).ToList().ForEach(c =>
            {
                if (c.CourseGroups.All(cg => cg.courseGroupStatus.Equals(CourseGroupStatus.CANCELED.Value)))
                {
                    c.courseStatus = CourseStatus.CANCELED.Value;
                    _coursesRepository.Update(c);
                }
            });
            return Task.CompletedTask;
        }
    }
}