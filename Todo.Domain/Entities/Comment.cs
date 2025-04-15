namespace Todo.Domain.Entities;
public class Comment : BaseEntity
{
    public string Text { get; set; }
    public DateTime CreationDate { get; set; }
    public virtual TodoItem TodoItem { get; set; }
    public virtual User User { get; set; }
}
