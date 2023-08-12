using Demo.Data;
using Demo.Dtos.Book;
using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public BooksController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await context.Books.Select(x => new GetBookDto
            {
                BookId = x.BookId,
                Title = x.Title,
                Description = x.Description,
            }).ToListAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.BookId == id);
            
            if (book is null) 
            {
                return NotFound();
            }

            GetBookDto bookDto = new()
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
            };

            return Ok(bookDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Description = dto.Description,
            };

            context.Books.Add(book);

            try
            {
                await context.SaveChangesAsync();
                return CreatedAtAction("GetBook", new { id = book.BookId }, book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, AddBookDto dto)
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.BookId == id);
            if (book is null)
            {
                return NotFound();
            }

            book.Title = dto.Title;
            book.Description = dto.Description;

            // context.Entry(book).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DelteBook(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.BookId == id);
            if (book is null)
            {
                return NotFound();
            }

            context.Books.Remove(book);

            try
            {
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
