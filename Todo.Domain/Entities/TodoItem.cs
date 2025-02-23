namespace Todo.Domain.Entities;
public class TodoItem : BaseEntity<int>
{
    //public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; }
    public DateTime Deadline { get;set; }
    public DateTime CreationTime { get; set; }
    public virtual Category Category { get; set; }
    public virtual User User { get; set; }
}
