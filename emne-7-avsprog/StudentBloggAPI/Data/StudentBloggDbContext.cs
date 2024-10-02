using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Features.Comments;
using StudentBloggAPI.Features.Posts;
using StudentBloggAPI.Features.Users;

namespace StudentBloggAPI.Data;

public class StudentBloggDbContext : DbContext
{
    // kjempe viktig
    public StudentBloggDbContext(DbContextOptions<StudentBloggDbContext> options)
    : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(p => p.Email).IsRequired();
            
            // uniqe
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.UserName).IsUnique();
        });

    }
}