using Microsoft.EntityFrameworkCore;
using MinimalistTasks.Domain.Interface;

namespace MinimalistTasks.Domain.Context;

public class MinimalistTasksContext : DbContext
{
    public DbSet<IUser> Users { get; set; }
    public DbSet<ITodo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO
    }
}