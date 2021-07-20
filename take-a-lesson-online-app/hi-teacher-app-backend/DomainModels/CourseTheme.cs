using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.Models
{
    public class CourseTheme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseThemeId { get; set; }
        public string CourseThemeName { get; set; }
        public string CourseThemeDescription { get; set; }
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public CourseTheme() { }
        public CourseTheme(string CourseThemeName, string CourseThemeDescription)
        {
            this.CourseThemeName = CourseThemeName;
            this.CourseThemeDescription = CourseThemeDescription;
        }

    }
}
