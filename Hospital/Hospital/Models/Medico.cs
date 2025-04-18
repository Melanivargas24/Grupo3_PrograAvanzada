using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Medico
    {
        [Key]
        public int IdMedico { get; set; }

        public int IdUsuario { get; set; }

        public int IdEspecialidad { get; set; }

        public Especialidad Especialidad { get; set; } = null!;

        public Usuario Usuario { get; set; } = null!;


        public ICollection<Cita> Citas { get; } = new List<Cita>();
    }
}
