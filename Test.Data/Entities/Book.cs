using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Primitives;

namespace Test.Data.Entities
{
    public class Book : BaseEntity
    {
        public required string Name { get; set; }
        public BookGenreEnum BookGenre { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}
