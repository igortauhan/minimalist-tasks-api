using Microsoft.EntityFrameworkCore;
using MinimalistTasks.Domain.Model;

namespace MinimalistTasks.Domain.Context;

public class MinimalistTasksContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseNpgsql(configurationRoot.GetConnectionString("DefaultConnection"));
    }
}