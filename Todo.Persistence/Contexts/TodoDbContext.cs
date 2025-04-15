using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Persistence.Contexts;
public class TodoDbContext(DbContextOptions<TodoDbContext> options) : IdentityDbContext(options)
{ 
    public DbSet<Project> Projects { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<Category> Categories { get; set; }
}

