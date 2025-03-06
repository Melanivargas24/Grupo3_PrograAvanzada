using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CasoPractico.Models;

public partial class SnakeGameContext : DbContext
{
    public SnakeGameContext()
    {
    }

    public SnakeGameContext(DbContextOptions<SnakeGameContext> options)
        : base(options)
    {
    }
    public virtual DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Server=DESKTOP-UJNOQQC;Database=SnakeGame;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username)
                .HasMaxLength(50)
                .IsRequired();
            entity.Property(u => u.Pass)
                .HasMaxLength(200)
                .IsRequired();
            entity.Property(u => u.Imagen)
                .IsRequired();
            entity.Property(u => u.Id)
                .ValueGeneratedOnAdd(); 
        });
    }
}
