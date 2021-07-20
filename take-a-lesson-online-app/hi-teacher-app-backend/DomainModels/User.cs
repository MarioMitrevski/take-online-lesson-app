
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace hi_teacher_app_backend.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string BirthDate { get; set; }
        public string ImageUrl { get; set; }

        public string Role { get; set; }

        public Teacher Teacher { get; set; }
        public Student Student { get; set; }

    }
}
