using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Primitives;

namespace Test.Data.DTO
{
    public class BookFilterDTO
    {
        public BookGenreEnum? Genre { get; set; }
        public string? Title { get; set; }

    }
}
