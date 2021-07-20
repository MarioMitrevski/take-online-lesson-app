using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.DTOs
{
    public class CategoriesQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int? CategoryId { get; set; }
        public string Search { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
}
