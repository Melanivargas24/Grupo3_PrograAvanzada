using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;

namespace Hospital.Pages.Admin.Citas
{
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
            Cita = await _context.Citas
                .Include(c => c.Paciente)
                .ThenInclude(p => p.Usuario)
                .Include(c => c.Especialidad)
                .Include(c => c.Estado)
                .Include(c => c.Medico)
                .ThenInclude(m => m.Usuario)
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

            return RedirectToPage("/Admin/Citas/Detalle");
        }

    }
}