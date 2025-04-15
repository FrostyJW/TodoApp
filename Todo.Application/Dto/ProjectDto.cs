namespace Todo.Application.Dto;
public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } 
    public DateTime Deadline { get; set; }
    public List<TodoItemDto> Todos { get; set; }
}

