using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Primitives;

namespace Test.Data.DTO
{
    public class BookDTO
    {
        public required string Name { get; set; }
        public BookGenreEnum Genre { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
