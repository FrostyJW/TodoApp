namespace Todo.Domain.Entities;
public class User : BaseEntity<int>
{
    public string Email { get; set; }
    public string Role { get; set; }
    public string PasswordHash { get; set; }
    public virtual List<TodoItem> Todos { get; set; }
    public virtual List<Project> Projects { get; set; }
}
