using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public List<Usuario> Usuarios { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string Busqueda { get; set; }

        public void OnGet()
        {
            Usuarios = _context.Usuarios
                .Where(u => string.IsNullOrEmpty(Busqueda) ||
                            u.Nombre.Contains(Busqueda) ||
                            u.Email.Contains(Busqueda))
                .ToList();
        }

        public IActionResult OnPost(int pacienteId)
        {
            return RedirectToPage("/Admin/Citas/Agendar", new { pacienteId });
        }
    }
}