using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Models;
using System.Security.Claims;

namespace Hospital.Pages.Admin.Citas
{
    public class AgendarModel : PageModel
    {
        private readonly Hospital.Models.HospitalDBContext _context;

        public AgendarModel(Hospital.Models.HospitalDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? pacienteId)
        {
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "IdEspecialidad", "Nombre");
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "Nombre");
            ViewData["IdMedico"] = new SelectList(_context.Medicos, "IdMedico", "IdMedico");

            // Asegúrate de inicializar Cita
            Cita = new Cita();

            if (pacienteId.HasValue)
            {
                Cita.IdPaciente = pacienteId.Value;
            }

            return Page();
        }



        [BindProperty]
        public Cita Cita { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                return Page();
            }
            Cita.IdEstado = 1;
            _context.Citas.Add(Cita);
            await _context.SaveChangesAsync();

            TempData["CitaAgendada"] = true;

            return RedirectToPage("/Citas/Agendar");
        }
    }
}
