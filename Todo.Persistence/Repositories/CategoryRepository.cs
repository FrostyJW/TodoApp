using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Persistence.Contexts;
using Todo.Persistence.Interfaces;

namespace Todo.Persistence.Repositories;
public class CategoryRepository(TodoDbContext context) : ICategoryRepository
{
    public async Task AddAsync(Category entity)
    {
        await context.Categories.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);
        if (category is not null)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        return await context.Categories.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Update(Category entity)
    {
        var category = await context.Categories.FirstOrDefaultAsync(p => p.Id == entity.Id);
        if (category is null)
        {
            return;
        }
        category.Title = entity.Title;

        await context.SaveChangesAsync();
    }
}
