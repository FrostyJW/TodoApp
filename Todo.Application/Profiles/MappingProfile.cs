using AutoMapper;
using Todo.Application.Dto;
using Todo.Domain.Entities;

namespace Todo.Application.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<TodoItem, TodoItemDto>().ReverseMap();
    }
}
