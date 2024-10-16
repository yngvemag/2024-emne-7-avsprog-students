namespace StudentBloggAPI.Configurations;

public class BasicAuthenticationOptions
{
    public List<string> ExcludePatterns { get; set; } = new List<string>();
}