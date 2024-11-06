namespace HATEOASBasics.Models;

public class BookDto : HalDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
}