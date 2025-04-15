using Microsoft.AspNetCore.Identity;

namespace Todo.Application.Dto;
public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public IdentityRole Role { get; set; }
    public List<TodoItemDto> Todos { get; set; }
    public List<ProjectDto> Projects { get; set; }

}
