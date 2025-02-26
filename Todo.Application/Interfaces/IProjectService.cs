using Todo.Application.Dto;

namespace Todo.Application.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetAllAsync();

    }
}
