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
            Console.WriteLine(">>>>> CADENA DE CONEXIÓN UTILIZADA:");
            Console.WriteLine(this.Database.GetDbConnection().ConnectionString);
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Expediente> Expedientes { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   //=> optionsBuilder.UseSqlServer("Server=DESKTOP-UJNOQQC;Database=Hospital;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Expediente>()
                .HasOne(e => e.Paciente)
                .WithMany(p => p.Expedientes)
                .HasForeignKey(e => e.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

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


            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC0778EF21D7");

                entity.HasIndex(e => e.Nombre, "UQ__Usuarios__72AFBCC6A9D12EAB").IsUnique();

                entity.HasIndex(e => e.Apellido, "UQ__Usuarios__95E042A6D83CF3F2").IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D1053410360562").IsUnique();

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .HasColumnName("apellido");
                entity.Property(e => e.Clave).HasMaxLength(255);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");
                entity.Property(e => e.Role).HasMaxLength(20);
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
