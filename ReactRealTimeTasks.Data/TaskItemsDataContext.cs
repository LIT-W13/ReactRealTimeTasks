using Microsoft.EntityFrameworkCore;

namespace ReactRealTimeTasks.Data;

public class TaskItemsDataContext : DbContext
{
    private readonly string _connectionString;

    public TaskItemsDataContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Entity<TaskItem>()
              .HasOne(t => t.User)
              .WithMany(u => u.TaskItems)
              .HasForeignKey(t => t.HandledBy);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
}