using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Pages.Expedientes
{
   //[Authorize(Roles = "empleado")]
    public class IndexModel : PageModel
    {
        private readonly HospitalDBContext _context;
        public List<Expediente> Expedientes { get; set; }

        public IndexModel(HospitalDBContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Expedientes = await _context.Expedientes
             .Include(e => e.Paciente)
             .ThenInclude(p => p.Usuario)
              .ToListAsync();

        }
    }
}