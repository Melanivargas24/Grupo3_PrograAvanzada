using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Pages.Admin
{
    // Solo los usuarios con el rol "Medico" pueden acceder a esta página
    [Authorize(Roles = "Medico")]
    public class MedicosModel : PageModel
    {
        private readonly HospitalDBContext _context;

        public MedicosModel(HospitalDBContext context)
        {
            _context = context;
        }

        // Lista de usuarios para mostrar en la vista
        public IList<Medico> Medicos { get; set; }

        public void OnGet()
        {
            // Obtenemos todos los usuarios de la base de datos
            Medicos = _context.Medicos.Include(m => m.Usuario).ToList();
            Medicos = _context.Medicos.Include(m => m.Especialidad).ToList();
        }
    }
}