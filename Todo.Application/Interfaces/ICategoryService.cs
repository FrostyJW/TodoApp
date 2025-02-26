using Todo.Application.Dto;

namespace Todo.Application.Interfaces;
public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
}
