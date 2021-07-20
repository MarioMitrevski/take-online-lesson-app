using Microsoft.EntityFrameworkCore;
namespace hi_teacher_app_backend.Models
{
    public class HiTeacherDBContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseTheme> CourseThemes { get; set; }
        public DbSet<DateTimeSlot> DateTimeSlots { get; set; }
        public DbSet<User> Users { get; set; }

        public HiTeacherDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student>()
                .HasMany(x => x.StudentCourseGroups)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId);// many to many

            modelBuilder.Entity<CourseGroup>()
                .HasMany(x => x.StudentCourseGroups)
                .WithOne(x => x.CourseGroup)
                .HasForeignKey(x => x.CourseGroupId); // many to many


            modelBuilder.Entity<Course>()
                .HasMany(x => x.CourseThemes)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId);// one to many

            modelBuilder.Entity<Course>()
                .HasMany(x => x.CourseGroups)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId);// one to many

            modelBuilder.Entity<CourseGroup>()
                .HasMany(x => x.DateTimeSlots)
                .WithOne(x => x.CourseGroup)
                .HasForeignKey(x => x.CourseGroupId);// one to many

            modelBuilder.Entity<User>()
                .HasOne(x => x.Teacher)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.NoAction);
            // one to one

            modelBuilder.Entity<User>()
                .HasOne(x => x.Student)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.NoAction);// one to one
            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
         
            modelBuilder.Entity<Category>()
               .HasData(
                   new Category
                   {
                       CategoryId = 1,
                       CategoryName = "Artificial Intelligence"
                   },
                   new Category
                   {
                       CategoryId = 2,
                       CategoryName = "Machine Learning"
                   },
                   new Category
                   {
                       CategoryId = 3,
                       CategoryName = "Natural Language Processing"
                   },
                   new Category
                   {
                       CategoryId = 4,
                       CategoryName = "Structural programming"
                   },
                   new Category
                   {
                       CategoryId = 5,
                       CategoryName = "OOP Concepts"
                   },
                   new Category
                   {
                       CategoryId = 6,
                       CategoryName = "OOP Programming"
                   },
                   new Category
                   {
                       CategoryId = 7,
                       CategoryName = "Architecture & Organization of Computers"
                   },
                   new Category
                   {
                       CategoryId = 8,
                       CategoryName = "Sensor systems"
                   },
                   new Category
                   {
                       CategoryId = 9,
                       CategoryName = "Computer animation"
                   },
                   new Category
                   {
                       CategoryId = 10,
                       CategoryName = "Visualization"
                   },
                   new Category
                   {
                       CategoryId = 11,
                       CategoryName = "C++"
                   },
                   new Category
                   {
                       CategoryId = 12,
                       CategoryName = "C"
                   },
                   new Category
                   {
                       CategoryId = 13,
                       CategoryName = "JAVA"
                   },
                   new Category
                   {
                       CategoryId = 14,
                       CategoryName = "Python"
                   },
                   new Category
                   {
                       CategoryId = 15,
                       CategoryName = "JavaScript"
                   },
                   new Category
                   {
                       CategoryId = 16,
                       CategoryName = "Digital Marketing"
                   },
                   new Category
                   {
                       CategoryId = 17,
                       CategoryName = "Marketing"
                   },
                   new Category
                   {
                       CategoryId = 18,
                       CategoryName = "Management"
                   },
                   new Category
                   {
                       CategoryId = 19,
                       CategoryName = "Economics"
                   },
                   new Category
                   {
                       CategoryId = 20,
                       CategoryName = "Business"
                   },
                   new Category
                   {
                       CategoryId = 21,
                       CategoryName = "Business & Statistics"
                   },
                   new Category
                   {
                       CategoryId = 22,
                       CategoryName = "Accounting"
                   },
                   new Category
                   {
                       CategoryId = 23,
                       CategoryName = "Bookkeeping"
                   },
                   new Category
                   {
                       CategoryId = 24,
                       CategoryName = "Mathematics"
                   },
                   new Category
                   {
                       CategoryId = 25,
                       CategoryName = "Mathematics for software engineers"
                   },
                   new Category
                   {
                       CategoryId = 26,
                       CategoryName = "Mathematics for economists"
                   },
                   new Category
                   {
                       CategoryId = 27,
                       CategoryName = "Calculus"
                   },
                   new Category
                   {
                       CategoryId = 28,
                       CategoryName = "Statistics"
                   },
                   new Category
                   {
                       CategoryId = 29,
                       CategoryName = "Discrete structures"
                   },
                   new Category
                   {
                       CategoryId = 30,
                       CategoryName = "Discrete Mathematics"
                   }
             );
        }

    }
}