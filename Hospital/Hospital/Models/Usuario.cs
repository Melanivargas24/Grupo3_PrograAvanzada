using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string Role { get; set; } = null!;

    public Medico? Medico { get; set; }
    public Paciente? Paciente { get; set; }

}
