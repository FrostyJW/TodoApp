using Todo.Application.Dto;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Persistence.Interfaces;

namespace Todo.Application.Services;
public class CommentService : ICommentService
{
    private readonly ICommentRepository commentRepository;
    public CommentService(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }
    public async Task AddAsync(CommentDto entity)
    {
        await commentRepository.AddAsync(new Comment()
        {
            Text = entity.Text,
            CreationDate = entity.CreationDate,
            TodoItem = new TodoItem()
            {
                Id = entity.TodoItem.Id,
                Title = entity.TodoItem.Title,
                Description = entity.TodoItem.Description,
                Priority = entity.TodoItem.Priority,
                Deadline = entity.TodoItem.Deadline,
                CreationTime = entity.TodoItem.CreationTime,
            },
        });
    }

    public async Task Delete(int id)
    {
        await commentRepository.Delete(id);
    }

    public async Task<IEnumerable<CommentDto>> GetAllAsync()
    {
        IEnumerable<Comment> enumerable = await commentRepository.GetAllAsync();
        return enumerable.Select(c => new CommentDto()
        {
            Id = c.Id,
            Text = c.Text,
            CreationDate = c.CreationDate,
            TodoItem = new TodoItemDto()
            {
                Id = c.TodoItem.Id,
                Title = c.TodoItem.Title,
                Description = c.TodoItem.Description,
                Priority = c.TodoItem.Priority,
                Deadline = c.TodoItem.Deadline,
                CreationTime = c.TodoItem.CreationTime,
            }
        });
    }

    public Task<CommentDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(CommentDto entity)
    {
        throw new NotImplementedException();
    }
}
