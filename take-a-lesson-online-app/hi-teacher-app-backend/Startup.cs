using System;
using System.Text;
using hi_teacher_app_backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

using hi_teacher_app_backend.repositories;
using hi_teacher_app_backend.repositories.impl;
using hi_teacher_app_backend.services;
using hi_teacher_app_backend.services.impl;
using hi_teacher_app_backend.DomainModels;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using Quartz;
using hi_teacher_app_backend.CronJobs;
using EmailService;

namespace hi_teacher_app_backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            services.AddScoped<IEmailSender, EmailSender>();

           /* services.AddQuartz(q =>
            {
                // base quartz scheduler, job and trigger configuration
                q.UseMicrosoftDependencyInjectionScopedJobFactory();

                var jobKey = new JobKey("CourseGroupMinParticipantsJob");

                // Register the job with the DI container
                q.AddJob<CourseGroupMinParticipantsJob>(opts => opts.WithIdentity(jobKey));

                // Create a trigger for the job
                q.AddTrigger(opts => opts
                   .ForJob(jobKey) 

                   .WithIdentity("CourseGroupMinParticipantsJob-trigger") // give the trigger a unique name
                   .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMinutes(1)).RepeatForever())); // run every 1 minute


                jobKey = new JobKey("CourseGroupSendGoogleMeetLinkJob");

                q.AddJob<CourseGroupSendGoogleMeetLinkJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                   .ForJob(jobKey)
                   .WithIdentity("CourseGroupSendGoogleMeetLinkJob-trigger") 
                   .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMinutes(1)).RepeatForever())); 
            });

            // ASP.NET Core hosting
            services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });*/





            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };

            });
            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.Teacher, Policies.TeacherPolicy());
                config.AddPolicy(Policies.Student, Policies.StudentPolicy());

            });

            services.AddCors(options =>
            {
                options.AddPolicy("Policy1",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader();
                    });
            });
            services.AddControllers();

            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                                            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );





            services.AddDbContext<HiTeacherDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("hi_teacher_app_backendContext")));

            //services.AddTransient<IRepository<Course>, CourseRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IUsersService<User>, UsersService>();
            services.AddTransient<ITeachersRepository, TeachersRepository>();
            services.AddTransient<IStudentsRepository, StudentsRepository>();
            services.AddTransient<ITeachersService<Teacher>, TeachersService>();
            services.AddTransient<IStudentsService<Student>, StudentsService>();

            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<ICategoriesService<Category>, CategoriesService>();

            services.AddTransient<ICoursesService<Course>, CoursesService>();
            services.AddTransient<ICoursesRepository, CoursesRepository>();

            services.AddTransient<ICourseGroupRepository, CourseGroupRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error");

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Images")),
                RequestPath = new PathString("/Images")
            });

        }
    }
}
