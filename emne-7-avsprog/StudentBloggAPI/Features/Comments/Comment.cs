using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentBloggAPI.Features.Posts;
using StudentBloggAPI.Features.Users;

namespace StudentBloggAPI.Features.Comments;

public class Comment
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey("PostId")]
    public Guid PostId { get; set; }

    [ForeignKey("UserId")]
    public Guid UserId { get; set; }
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    public DateTime DateCommented { get; set; }
    
    // Navigation properties
    public virtual Post? Post { get; set; }
    public virtual User? User { get; set; }
}