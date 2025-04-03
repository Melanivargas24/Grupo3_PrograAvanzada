using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Pages
{
    // Solo los usuarios con el rol "Administrador" pueden acceder a esta página
    [Authorize(Roles = "Administrador")]
    public class AdminModel : PageModel
    {
        private readonly HospitalDBContext _context;

        public AdminModel(HospitalDBContext context)
        {
            _context = context;
        }

        // Lista de usuarios para mostrar en la vista
        public IList<Usuario> Users { get; set; }

        public void OnGet()
        {
            // Obtenemos todos los usuarios de la base de datos
            Users = _context.Usuarios.ToList();
        }
    }
}
