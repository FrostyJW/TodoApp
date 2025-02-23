namespace Todo.Domain.Entities;
public class Project : BaseEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime Deadline { get; set; }
    public virtual List<TodoItem> Todos { get; set; }   
}
