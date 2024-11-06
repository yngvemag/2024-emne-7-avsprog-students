using HATEOASBasics.Models;
using Microsoft.AspNetCore.Mvc;

namespace HATEOASBasics.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly LinkGenerator _generator;

    private static readonly List<BookDto> Books =
    [
        new BookDto()
        {
            Id = 1,
            Title = "Ringenes Herre",
            Author = "J.R.R. Tolkien"
        }
    ];

    public BooksController(LinkGenerator generator)
    {
        _generator = generator;
    }
    #region GET SECTION OF BOOKS
    [HttpGet("GetBooks")]
    public ActionResult<IEnumerable<BookDto>> GetBooksAsync()
    {
        return Ok(Books);
    }
    
    [HttpGet("GetBooks/{id}")]
    public ActionResult<BookDto> GetBookById(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        
        if (book is null) return BadRequest("Book not found");

        // HATEOAS !!    
        book.Links.Add(
            new Link()
            {
                Href = _generator?.GetPathByAction(
                    HttpContext, 
                    action: nameof(GetBookById),
                    values: new { id = book.Id }),
                Rel = "self",
                Type = "GET"
            });
        
        book.Links.Add(
            new Link()
            {
                Href = _generator?.GetPathByAction(
                    HttpContext, 
                    action: "Get",
                    controller: "WeatherForecast"),
                Rel = "weatherforecast",
                Type = "GET"
            });
        
        

        return Ok(book);
    }
    
    #endregion
}