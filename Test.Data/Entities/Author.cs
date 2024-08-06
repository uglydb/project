using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Primitives;

namespace Test.Data.Entities
{
    public class Author : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
