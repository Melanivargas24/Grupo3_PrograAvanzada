using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Pages.Admin.Citas
{
    public class SeleccionarPacienteModel : PageModel
    {
        private readonly HospitalDBContext _context;

        public SeleccionarPacienteModel(HospitalDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<Paciente> Pacientes { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string Busqueda { get; set; }

        public void OnGet()
        {
            Pacientes = _context.Pacientes
        .Include(p => p.Usuario)
        .Where(u => string.IsNullOrEmpty(Busqueda) ||
                    u.Usuario != null && (u.Usuario.Nombre.Contains(Busqueda) || u.Usuario.Email.Contains(Busqueda)))
        .ToList();

        }

        public IActionResult OnPost(int pacienteId)
        {
            return RedirectToPage("/Admin/Citas/Agendar", new { pacienteId });
        }
    }
}