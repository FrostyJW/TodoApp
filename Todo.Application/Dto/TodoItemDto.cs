using Todo.Infrastructure.Enums;

namespace Todo.Application.Dto;
public class TodoItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime CreationTime { get; set; }
    public ProjectDto Project { get; set; }
}
