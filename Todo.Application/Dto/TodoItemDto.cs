namespace Todo.Application.Dto;
public class TodoItemDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime CreationTime { get; set; }
}
