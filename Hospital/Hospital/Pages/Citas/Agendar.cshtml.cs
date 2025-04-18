using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Pages.Citas
{
    public class AgendarModel : PageModel
    {
        private readonly Hospital.Models.HospitalDBContext _context;

        public AgendarModel(Hospital.Models.HospitalDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "IdEspecialidad", "Nombre");
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre");
            ViewData["IdMedico"] = new SelectList(
                _context.Medicos.Include(m => m.Usuario).Select(m => new
                {
                    IdMedico = m.IdMedico,
                    NombreCompleto = (m.Usuario.Nombre ?? "Sin nombre") + " " + (m.Usuario.Apellido ?? "Sin apellido")
                }).ToList(),
                "IdMedico",
                "NombreCompleto"
            );
            return Page();
        }

        [BindProperty]
        public Cita Cita { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "IdEspecialidad", "Nombre");
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre");

            var medicos = _context.Medicos
             .Include(m => m.Usuario)
                .Select(m => new
              { IdMedico = m.IdMedico,
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

          

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int idUsuario = int.Parse(userIdClaim.Value);

            var paciente = _context.Pacientes.FirstOrDefault(p => p.IdUsuario == idUsuario);

            if (paciente == null)
            {
                ModelState.AddModelError(string.Empty, "No se encontró un paciente vinculado a este usuario.");
                return Page();
            }

            Cita.IdPaciente = paciente.Id;
            Cita.IdEstado = 1;

            _context.Citas.Add(Cita);
            await _context.SaveChangesAsync();

            TempData["CitaAgendada"] = true;

            return RedirectToPage("/Citas/Agendar");
        }
    }
}