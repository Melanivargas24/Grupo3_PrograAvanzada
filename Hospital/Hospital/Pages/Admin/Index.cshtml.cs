using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Pages
{
    // Solo los usuarios con el rol "Medico" pueden acceder a esta página
    [Authorize(Roles = "Medico")]
    public class AdminModel : PageModel
    {
        private readonly HospitalDBContext _context;

        public AdminModel(HospitalDBContext context)
        {
            _context = context;
        }

        // Lista de usuarios para mostrar en la vista
        public IList<Paciente> Paciente { get; set; }

        public void OnGet()
        {
            // Obtenemos todos los usuarios de la base de datos
            Paciente = _context.Pacientes
             .Include(p => p.Usuario)
             .ToList();

        }
    }
}
