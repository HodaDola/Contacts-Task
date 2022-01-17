using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactNumbers.Models
{
    public class Pager
    {
        public int TotalItem { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public Pager()
        {

        }

        public Pager(int totalitems, int page, int pagesize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalitems / (decimal)pagesize);
            int currentPage = page;

            int startPage = currentPage - 5;
            int endPAge = currentPage + 4;

            if (startPage <= 0)
            {
                endPAge = endPAge - (startPage - 1);
                startPage = 1;
            }

            if (endPAge > totalPages)
            {
                endPAge = totalPages;
                if (endPAge > 10)
                {
                    startPage = endPAge - 9;
                }

                TotalItem = totalitems;
                CurrentPage = currentPage;
                PageSize = pagesize;
                TotalPages = totalPages;
                StartPage = startPage;
                EndPage = endPAge;
            }
        }

    }
}
