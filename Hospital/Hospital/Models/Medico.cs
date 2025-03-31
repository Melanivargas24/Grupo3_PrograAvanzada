using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Medico
    {
        [Key]
        public int IdMedico { get; set; }

        public int IdEspecialidad { get; set; }

        public int IdDireccion { get; set; }

        public string Telefono { get; set; } = null!;

        public Especialidad Especialidad { get; set; } = null!;

        public ICollection<Cita> Citas { get; } = new List<Cita>();
    }
}
