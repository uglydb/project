using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Repositories;
using Test.Data.Entities;
using Test.Data.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Test.Service.Author
{
    public interface ILibraryService
    {
        Task<PaginationResultDTO<AuthorDTO>> GetListAuthor(PaginationRequestDTO data);
        Task<BookSortByResultDTO<BookDTO>> GetSortedBooks(BookSortRequestDTO data);
        Task<BookSortByResultDTO<BookDTO>> GetListBook(BookFilterDTO data);
    }

    public class LibraryService : ILibraryService
    {
        private readonly IBaseRepo<Test.Data.Entities.Author> _authorRepository;
        private readonly IBaseRepo<Test.Data.Entities.Book> _bookRepository;
        public LibraryService(IBaseRepo<Data.Entities.Author> authorRepository, IBaseRepo<Book> bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        public async Task<PaginationResultDTO<AuthorDTO>> GetListAuthor(PaginationRequestDTO data)
        {
            var authors = await _authorRepository
                .GetQueryable(x => x.IsDeleted == false && x.Name.Contains(data.Name))
                .Select(x => new AuthorDTO
                {
                    Name = x.Name,
                    DateOfBirth = x.CreateDate,
                    Books = 
                        x.Books.Select(b => new BookDTO
                        {
                            Name = b.Name,
                            CreationDate = b.CreateDate,
                            Genre = b.BookGenre,
                        }).OrderBy(b => b.CreationDate).ToList()
                })
                .Skip(data.PageIndex * data.PageSize)
                .Take(data.PageSize)
                .ToListAsync();
            authors.OrderBy(x => x.Name);
            var count = await _authorRepository
                .GetQueryable(x => x.IsDeleted == false && x.Name.Contains(data.Name))
                .CountAsync();

            return new PaginationResultDTO<AuthorDTO>
            {
                Count = count,
                Lists = authors
            };
        }

        public async Task<BookSortByResultDTO<BookDTO>> GetSortedBooks(BookSortRequestDTO data)
        {
            var query = _bookRepository.GetQueryable(x => x.IsDeleted == false);

            // Применение сортировки на основе параметров
            if (!string.IsNullOrEmpty(data.SortBy))
            {
                switch (data.SortBy.ToLower())
                {
                    case "name":
                        query = data.SortOrder ? query.OrderBy(b => b.Name) : query.OrderByDescending(b => b.Name);
                        break;
                    case "genre":
                        query = data.SortOrder ? query.OrderBy(b => b.BookGenre) : query.OrderByDescending(b => b.BookGenre);
                        break;
                    case "creationdate":
                        query = data.SortOrder ? query.OrderBy(b => b.CreateDate) : query.OrderByDescending(b => b.CreateDate);
                        break;
                    default:
                        query = data.SortOrder ? query.OrderBy(b => b.Name) : query.OrderByDescending(b => b.Name);
                        break;
                }
            }

            var books = await query
                .Select(b => new BookDTO
                {
                    Name = b.Name,
                    Genre = b.BookGenre,
                    CreationDate = b.CreateDate
                })
                .ToListAsync();

            return new BookSortByResultDTO<BookDTO>
            {
                Lists = books,
                Count = books.Count
            };
        }


        public async Task<BookSortByResultDTO<BookDTO>> GetListBook(BookFilterDTO data)
        {
            var books = await _bookRepository
                .GetQueryable(x => x.IsDeleted == false && x.Name.Contains(data.Title) && x.BookGenre.Equals(data.Genre))
                .Select(x => new BookDTO
                {
                    Name = x.Name,
                    CreationDate = x.CreateDate,
                    Genre = x.BookGenre,
                })
                .ToListAsync();

            return new BookSortByResultDTO<BookDTO> { Lists = books };
        }
    }
}
