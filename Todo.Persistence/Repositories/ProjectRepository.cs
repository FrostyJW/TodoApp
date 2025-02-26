using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Persistence.Contexts;
using Todo.Persistence.Interfaces;

namespace Todo.Persistence.Repositories;
public class ProjectRepository(TodoDbContext context) : IProjectRepository
{
    public async Task AddAsync(Project entity)
    {
        await context.Projects.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var project = await context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        if (project is not null)
        {
            context.Projects.Remove(project);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await context.Projects.Include(p => p.Todos).Include(p => p.Users).ToListAsync();
    }

    public async Task<Project> GetByIdAsync(int id)
    {
        return await context.Projects.Include(p => p.Todos).Include(p => p.Users).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Update(Project entity)
    {
        var project = await context.Projects.Include(p => p.Todos).Include(p => p.Users).FirstOrDefaultAsync(p => p.Id == entity.Id);
        if (project is null)
        {
            return;
        }
        project.Name = entity.Name;
        project.Description = entity.Description;
        project.Status = entity.Status;
        project.Deadline = entity.Deadline;
        project.Todos = entity.Todos;
        project.Users = project.Users;

        await context.SaveChangesAsync();
    }
}
