using Todo.Persistence.Contexts;

namespace Todo.Persistence.Repositories;
public class AbstactRepository
{
    protected readonly TodoDbContext _context;
    public AbstactRepository(TodoDbContext context)
    {
        _context = context;
    }
}
