using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Estado
    {
        [Key]
        public int IdEstado { get; set; }

        public string Nombre { get; set; } = null!;

        public ICollection<Cita> Citas { get; } = new List<Cita>();
    }
}
