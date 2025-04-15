using Todo.Application.Dto;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Persistence.Interfaces;
using Todo.Persistence.Repositories;

namespace Todo.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }
    public async Task AddAsync(CategoryDto entity)
    {
        await categoryRepository.AddAsync(new Category()
        {
            Id = entity.Id,
            Title = entity.Title
        });
    }

    public async Task Delete(int id)
    {
        await categoryRepository.Delete(id);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        IEnumerable<Category> enumerable = await categoryRepository.GetAllAsync();
        return enumerable.Select(category => new CategoryDto()
        {
            Id = category.Id,
            Title = category.Title
        });
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        Category category = await categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return null;
        }
        else
        {
            return new CategoryDto()
            {
                Id = category.Id,
                Title = category.Title
            };
        }
    }
    public async Task Update(CategoryDto entity)
    {
        Category category = await categoryRepository.GetByIdAsync(entity.Id);
        if (category is null)
        {
            return;
        }
        else
        {
            category.Id = entity.Id;
            category.Title = entity.Title;
            await categoryRepository.Update(category);
        }
    }
}
