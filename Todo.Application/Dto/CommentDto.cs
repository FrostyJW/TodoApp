namespace Todo.Application.Dto;
public class CommentDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreationDate { get; set; }
    public TodoItemDto TodoItem { get; set; }
    public UserDto User { get; set; }
}
