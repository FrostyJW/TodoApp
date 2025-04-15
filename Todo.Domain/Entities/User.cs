using Microsoft.AspNetCore.Identity;

namespace Todo.Domain.Entities;
public class User : IdentityUser
{
    public virtual List<TodoItem> Todos { get; set; }
    public virtual List<Project> Projects { get; set; }
    public virtual IdentityRole Role { get; set; }
}
