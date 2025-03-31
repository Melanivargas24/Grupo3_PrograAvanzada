using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Especialidad
    {
        [Key]
        public int IdEspecialidad { get; set; }

        public string Nombre { get; set; } = null!;

        public ICollection<Medico> Medicos { get; } = new List<Medico>();

        public ICollection<Cita> Citas { get; } = new List<Cita>();
    }
}
