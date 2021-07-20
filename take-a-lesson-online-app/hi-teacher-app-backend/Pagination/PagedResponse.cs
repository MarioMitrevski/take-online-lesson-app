using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.Pagination
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public T Data { get; set; }


        public PagedResponse(T Data, int PageNumber, int PageSize, int TotalPages, int TotalRecords)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.Data = Data;
            this.TotalPages = TotalPages;
            this.TotalRecords = TotalRecords;
        }
    }
}
