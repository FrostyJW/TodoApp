﻿using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Infrastructure.Enums;
using Todo.Persistence.Contexts;
using Todo.Persistence.Interfaces;

namespace Todo.Persistence.Repositories;
public class TodoRepository(TodoDbContext context) : ITodoRepository
{
    public async Task AddAsync(TodoItem entity)
    {
        await context.TodoItems.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var todo = await context.TodoItems.FirstOrDefaultAsync(p => p.Id == id);
        if (todo is not null)
        {
            context.TodoItems.Remove(todo);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await context.TodoItems.ToListAsync();
    }

    public async Task<TodoItem> GetByIdAsync(int id)
    {
        return await context.TodoItems.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Update(TodoItem entity)
    {
        var todo = await context.TodoItems.FirstOrDefaultAsync(p => p.Id == entity.Id);
        if (todo is null)
        {
            return;
        }
        todo.Title = entity.Title;
        todo.Description = entity.Description;
        todo.Priority = entity.Priority;
        todo.Status = entity.Status;
        todo.Deadline = entity.Deadline;
        todo.CreationTime = entity.CreationTime;
        todo.Category = entity.Category;
        todo.User = entity.User;

        await context.SaveChangesAsync();
    }
    public async Task<IEnumerable<TodoItem>> FilterByDeadlineAsync()
    {
        var currentDate = DateTime.Now;
        return await context.TodoItems.Include(p => p.User).Include(p => p.Category).Where(p => p.Deadline < currentDate).ToListAsync();
    }
    public async Task<IEnumerable<TodoItem>> FilterByPriorityAsync(Priority priority)
    {
        return await context.TodoItems.Include(p => p.User).Include(p => p.Category).Where(p => p.Priority == priority).ToListAsync();
    }
    public async Task<IEnumerable<TodoItem>> FilterByStatusAsync(Status status)
    {
        return await context.TodoItems.Include(p => p.User).Include(p => p.Category).Where(p => p.Status == status).ToListAsync();
    }
    //FilterByDeadline
    //FilterByPriority
    //FilterByStatus
}
