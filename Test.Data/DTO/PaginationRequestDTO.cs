using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Data.DTO
{
    public class PaginationRequestDTO
    {
        public required int PageIndex { get; set;}
        public required int PageSize { get; set; }
        public string? Name { get; set; }
    }
}
