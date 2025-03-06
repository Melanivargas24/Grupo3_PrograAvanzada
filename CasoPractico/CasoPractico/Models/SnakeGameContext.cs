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
    public virtual DbSet<GameHistory> GameHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Server=MSI;Database=SnakeGame;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<GameHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GameHistory");

            entity.ToTable("GameHistory");

            entity.Property(e => e.GameDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
