using Microsoft.AspNetCore.Identity;

namespace Todo.Domain.Entities;
public class User : IdentityUser
{
    public virtual List<TodoItem> Todos { get; set; }
}
