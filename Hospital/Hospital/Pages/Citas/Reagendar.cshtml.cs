using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;

namespace Hospital.Pages.Citas
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

            var cita = await _context.Citas.FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }
            Cita = cita;

            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "IdEspecialidad", "Nombre");
            ViewData["IdMedico"] = new SelectList(
                _context.Medicos.Include(m => m.Usuario).Select(m => new
                {
                    IdMedico = m.IdMedico,
                    NombreCompleto = (m.Usuario.Nombre ?? "Sin nombre") + " " + (m.Usuario.Apellido ?? "Sin apellido")
                }).ToList(),
                "IdMedico",
                "NombreCompleto"
            );
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "IdEspecialidad", "Nombre");
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre");

            var medicos = _context.Medicos
             .Include(m => m.Usuario)
                .Select(m => new
                {
                    IdMedico = m.IdMedico,
                    NombreCompleto = (m.Usuario.Nombre ?? "Sin nombre") + " " + (m.Usuario.Apellido ?? "Sin apellido")
                }).ToList();

            ViewData["IdMedico"] = new SelectList(medicos, "IdMedico", "NombreCompleto", Cita.IdMedico);

            if (Cita.Fecha < DateTime.Today)
            {
                ModelState.AddModelError("Cita.Fecha", "Elija un día hábil");
                return Page();
            }

            if (Cita.Fecha.DayOfWeek == DayOfWeek.Saturday || Cita.Fecha.DayOfWeek == DayOfWeek.Sunday)
            {
                ModelState.AddModelError("Cita.Fecha", "Elija un día hábil (Entre Lunes y Viernes)");
                return Page();
            }

            if (Cita.Hora < new TimeSpan(8, 0, 0) || Cita.Hora > new TimeSpan(16, 0, 0))
            {
                ModelState.AddModelError("Cita.Hora", "La hora debe estar en un rango de 8:00 a.m. y 4:00 p.m.");
                return Page();
            }

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
