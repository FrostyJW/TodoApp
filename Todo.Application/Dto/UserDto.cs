namespace Todo.Application.Dto;
public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public List<TodoItemDto> Todos { get; set; }
}
