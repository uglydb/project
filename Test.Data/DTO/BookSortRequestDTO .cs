using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Primitives;

namespace Test.Data.DTO
{
    public class BookSortRequestDTO
    {
        public string SortBy { get; set; }
        public bool SortOrder { get; set; }
    }
}
