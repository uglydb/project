using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Data.DTO
{
    public class BookSortByResultDTO<T> where T : class
    {
        public IEnumerable<T>? Lists { get; set; }
        public int? Count { get; set; }
    }
}
