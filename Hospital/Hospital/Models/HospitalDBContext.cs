using Microsoft.EntityFrameworkCore;

namespace Hospital.Models
{
    public partial class HospitalDBContext : DbContext
    {
        public HospitalDBContext()
        {
        }

        public HospitalDBContext(DbContextOptions<HospitalDBContext> options)
            : base(options)
        {
        }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Estado> Estados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   => optionsBuilder.UseSqlServer("Server=DESKTOP-UJNOQQC;Database=Hospital;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Especialidad)
                .WithMany(c => c.Citas)
                .HasForeignKey(c => c.IdEspecialidad)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Medico)
                .WithMany(c => c.Citas)
                .HasForeignKey(c => c.IdMedico)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Estado)
                .WithMany(c => c.Citas)
                .HasForeignKey(c => c.IdEstado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Medico>()
                .HasOne(m => m.Especialidad)
                .WithMany(m => m.Medicos)
                .HasForeignKey(m => m.IdEspecialidad)
                .OnDelete(DeleteBehavior.Restrict);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
