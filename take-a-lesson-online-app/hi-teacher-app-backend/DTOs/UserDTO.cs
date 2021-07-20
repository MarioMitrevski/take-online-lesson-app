using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.DTOs
{
    public class UserDTO
    {
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string ImageUrl { get; set; }
        public string JwtToken { get; set; }
    }
}
