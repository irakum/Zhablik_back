using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Zhablik.Models;

namespace Zhablik.Data;

public class AppDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<FrogInfo>? Frogs { get; set; }
    public DbSet<Assignment>? Tasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne()
            .HasForeignKey(a => a.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Frogs)
            .WithOne(uf => uf.User)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }

}