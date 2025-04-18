using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; } = null!;

        public ICollection<Expediente> Expedientes { get; set; } = new List<Expediente>();

        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}