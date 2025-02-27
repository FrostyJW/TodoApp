using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Persistence.Contexts;
using Todo.Persistence.Interfaces;

namespace Todo.Persistence.Repositories
{
    public class CommentRepository(TodoDbContext context) : ICommentRepository
    {
        public async Task AddAsync(Comment entity)
        {
            await context.Comments.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var comment = await context.Comments.FirstOrDefaultAsync(p => p.Id == id);
            if(comment is not null)
            {
                context.Comments.Remove(comment);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await context.Comments.Include(p => p.User).Include(p => p.TodoItem).ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await context.Comments.Include(p => p.User).Include(p => p.TodoItem).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Update(Comment entity)
        {
            var comment = await context.Comments.FirstOrDefaultAsync(p => p.Id == entity.Id);
            if (comment is null)
            {
                return;
            }
            comment.Text = entity.Text;
            comment.CreationDate = entity.CreationDate;
            comment.User = entity.User;
            comment.TodoItem = entity.TodoItem;

            await context.SaveChangesAsync();
        }
    }
}
