namespace PersonRestAPI.Models;

public class Person 
{
    public long id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; } 
}