using Microsoft.AspNetCore.Mvc;
using Test.Data.DTO;
using Test.Service.Author;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        public AuthorController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }


        // POST api/<AuthorController>
        [HttpPost("getAuthor")]
        public async Task<IActionResult> GetListAuthor([FromBody] PaginationRequestDTO data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _libraryService.GetListAuthor(data);
            return Ok(response);
        }

        [HttpPost("getSortedBooks")]
        public async Task<IActionResult> GetSortedBooks([FromBody] BookSortRequestDTO data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _libraryService.GetSortedBooks(data);
            return Ok(response);
        }

        [HttpPost("GetFilteredBooks")]
        public async Task<IActionResult> GetListBook([FromBody] BookFilterDTO data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _libraryService.GetListBook(data);
            return Ok(response);
        }

        [HttpPost("GetSortedAndFilteredBooks")]
        public async Task<IActionResult> GetSortedAndFilteredBooks([FromBody] BookSortFilterRequestDTO data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _libraryService.GetSortedAndFilteredBooks(data);
            return Ok(response);
        }
    }
}
