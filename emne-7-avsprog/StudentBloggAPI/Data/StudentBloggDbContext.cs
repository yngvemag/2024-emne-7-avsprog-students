using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Features.Comments;
using StudentBloggAPI.Features.Posts;
using StudentBloggAPI.Features.Users;

namespace StudentBloggAPI.Data;

public class StudentBloggDbContext : DbContext
{
    public StudentBloggDbContext(DbContextOptions<StudentBloggDbContext> options)
    : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
}