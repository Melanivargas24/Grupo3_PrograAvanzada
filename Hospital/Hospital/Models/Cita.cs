using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }

        public int IdPaciente {  get; set; }

        public int IdEspecialidad { get; set; }

        public int IdMedico { get; set; }

        public DateOnly Fecha { get; set; }

        public TimeOnly Hora { get; set; }

        public int IdEstado { get; set; }

        public Especialidad Especialidad { get; set; } = null!;

        public Medico Medico { get; set; } = null!;

        public Estado Estado { get; set; } = null!;

    }
}
