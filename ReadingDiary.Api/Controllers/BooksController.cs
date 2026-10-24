using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadingDiary.Api.Data;
using ReadingDiary.Api.Models;

namespace ReadingDiary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly ReadingDiaryDbContext _context;

    public BooksController(ReadingDiaryDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks(string? search)
    {
        var query = _context.Books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(b =>
                b.Title.Contains(search) ||
                b.Author.Contains(search));
        }

        return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(Book book)
    {
        _context.Books.Add(book);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetBook),
            new { id = book.Id },
            book
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, Book book)
    {
        var existingBook = await _context.Books.FindAsync(id);

        if (existingBook == null)
        {
            return NotFound();
        }

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.Status = book.Status;
        existingBook.CurrentPage = book.CurrentPage;
        existingBook.TotalPages = book.TotalPages;
        existingBook.Rating = book.Rating;
        existingBook.Notes = book.Notes;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}

    