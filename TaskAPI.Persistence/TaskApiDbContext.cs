using Microsoft.EntityFrameworkCore;
using TaskAPI.Persistence.Configurations;
using TaskAPI.Persistence.Entities;

namespace TaskAPI.Persistence;

public class TaskApiDbContext(DbContextOptions<TaskApiDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CompanyEntity> Companies { get; set; }
}