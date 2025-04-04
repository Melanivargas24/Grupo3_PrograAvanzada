using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class Expediente
    {
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        public Paciente Paciente { get; set; } = null!;

        [Required]
        public string Diagnostico { get; set; } = string.Empty;

        [Required]
        public string Tratamientos { get; set; } = string.Empty;

        [Required]
        public string Medicamentos { get; set; } = string.Empty;

        public string? Historial { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

    }
}