using System.ComponentModel.DataAnnotations;
using StudentBloggAPI.Features.Comments;
using StudentBloggAPI.Features.Posts;

namespace StudentBloggAPI.Features.Users;


public readonly record struct UserId(Guid Value)
{
    public static UserId NewId => new UserId(Guid.NewGuid());
    public static UserId Empty => new UserId(Guid.Empty);
} 

/* DbContext -> protected override void OnModelCreating(ModelBuilder modelBuilder)
 *    //    modelBuilder.Entity<User>()
    //        .Property(x => x.Id)
    //        .HasConversion(
    //            id => id.userId,// Fra UserId til GUID (for lagring i DB)

    //            // Konverterer fra en nullable Guid til UserId når du leser fra databasen.
    //            // Benytter UserId.Empty for null verdier for å sikre at du alltid får en UserId instans.
    //            value => new UserId(value)
    //        );
    //}
 * 
 */

public class User
{
    [Key]
    public Guid Id { get; set; }
    //public UserId Id { get; set; }
    
    [Required]
    [MinLength(3), MaxLength(30)]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    [MinLength(2), MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MinLength(2), MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string HashedPassword { get; set; } = string.Empty;
    
    [Required]
    public DateTime Created { get; set; }
    
    [Required]
    public DateTime Updated { get; set; }
    
    [Required]
    public bool IsAdminUser { get; set; }
    
    // Navigation properties
    public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
}