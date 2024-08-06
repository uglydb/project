using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Data.DTO
{
    public class AuthorDTO
    {
        public required string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<BookDTO> Books { get; set; }
    }
}
