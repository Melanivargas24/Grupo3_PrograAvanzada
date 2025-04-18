using System.Security.Claims; 
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Pages.Citas
{
    [Authorize] 
    public class DetalleModel : PageModel
    {
        private readonly Hospital.Models.HospitalDBContext _context;

        public DetalleModel(Hospital.Models.HospitalDBContext context)
        {
            _context = context;
        }

        public IList<Cita> Cita { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim))
            {
                Cita = new List<Cita>();
                return;
            }
            int idUsuario = int.Parse(userIdClaim);

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);

            if (paciente == null)
            {
                Cita = new List<Cita>();
                return;
            }

            Cita = await _context.Citas
             .Include(c => c.Paciente)
                 .ThenInclude(p => p.Usuario)
             .Include(c => c.Especialidad)
             .Include(c => c.Estado)
             .Include(c => c.Medico)
                .ThenInclude(m => m.Usuario)
             .Where(c => c.IdPaciente == paciente.Id)
             .ToListAsync();

        }

        public async Task<IActionResult> OnPostAsync(int idCita)
        {
            var cita = await _context.Citas.FirstOrDefaultAsync(c => c.IdCita == idCita);

            if (cita == null)
            {
                return NotFound();
            }

            cita.IdEstado = 2;
            await _context.SaveChangesAsync();

            return RedirectToPage("/Citas/Detalle");
        }
    }
}
