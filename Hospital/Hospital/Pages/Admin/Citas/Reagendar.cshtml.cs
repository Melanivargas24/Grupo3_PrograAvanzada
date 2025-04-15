using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;

namespace Hospital.Pages.Admin.Citas
{
    public class ReagendarModel : PageModel
    {
        private readonly Hospital.Models.HospitalDBContext _context;

        public ReagendarModel(Hospital.Models.HospitalDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cita Cita { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita =  await _context.Citas.FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }
            Cita = cita;
           ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "IdEspecialidad", "IdEspecialidad");
           ViewData["IdMedico"] = new SelectList(_context.Medicos, "IdMedico", "IdMedico");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }

            Cita.IdEstado = 1;
            _context.Attach(Cita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(Cita.IdCita))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Citas/Detalle");
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }
    }
}
