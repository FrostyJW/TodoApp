namespace Todo.Domain.Entities;
public class Project : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime Deadline { get; set; }
    public virtual List<TodoItem> Todos { get; set; }
    public virtual List<User> Users { get; set; }
}
