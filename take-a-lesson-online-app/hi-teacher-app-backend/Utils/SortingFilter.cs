using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.Utils
{
    public class SortingFilter
    {
        public string SortBy { get; set; }
        public string OrderBy { get; set; }

        public SortingFilter(string SortBy, string OrderBy)
        {
            if (SortBy == "Price" || SortBy == "Date")
            {
                this.SortBy = SortBy;
            }
            else
            {
                this.SortBy = "Date";
            }

            if (OrderBy == "Asc" || OrderBy == "Desc")
            {
                this.OrderBy = OrderBy;
            }
            else
            {
                this.OrderBy = "Asc";
            }
        }

        public bool IsPriceAscending()
        {
            return SortBy == "Price" && OrderBy == "Asc";
        }

        public bool IsPriceDescending()
        {
            return SortBy == "Price" && OrderBy == "Desc";
        }

        public bool IsDateAscending()
        {
            return SortBy == "Date" && OrderBy == "Asc";
        }

        public bool IsDateDescending()
        {
            return SortBy == "Date" && OrderBy == "Desc";
        }
    }
}
