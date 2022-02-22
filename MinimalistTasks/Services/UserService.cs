using MinimalistTasks.Domain.Context;
using MinimalistTasks.Domain.Interface;
using MinimalistTasks.Domain.Model;

namespace MinimalistTasks.Services;

public class UserService
{
    private readonly MinimalistTasksContext _context;

    public UserService(MinimalistTasksContext context)
    {
        _context = context;
    }

    public async Task<IUser> Insert(User user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
}