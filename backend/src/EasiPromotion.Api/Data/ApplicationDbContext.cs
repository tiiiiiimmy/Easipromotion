using Microsoft.EntityFrameworkCore;
using EasiPromotion.Api.Models;

namespace EasiPromotion.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Poster> Posters => Set<Poster>();
    public DbSet<ProductData> Products => Set<ProductData>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Configure Role
        modelBuilder.Entity<Role>()
            .HasIndex(r => r.Name)
            .IsUnique();

        // Configure Poster
        modelBuilder.Entity<Poster>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure ProductData
        modelBuilder.Entity<ProductData>()
            .HasOne(p => p.Poster)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.PosterId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed Roles
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = Role.Names.NonMember },
            new Role { Id = 2, Name = Role.Names.Member },
            new Role { Id = 3, Name = Role.Names.Admin }
        );
    }
} 