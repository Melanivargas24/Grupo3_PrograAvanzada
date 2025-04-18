using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;

namespace Hospital.Pages.Admin
{
    public class editMedicoModel : PageModel
    {
        private readonly Hospital.Models.HospitalDBContext _context;

        public editMedicoModel(Hospital.Models.HospitalDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Medico Medico { get; set; } = default!;

        public List<Especialidad> Especialidades { get; set; } = new();


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
              .Include(m => m.Usuario)
              .FirstOrDefaultAsync(m => m.IdMedico == id);

            if (medico == null)
            {
                return NotFound();
            }
            Medico = medico;
            Especialidades = await _context.Especialidades.ToListAsync();

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

            // Buscar el medico actual incluyendo el Usuario
            var medico = await _context.Medicos
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.IdMedico == Medico.IdMedico);

            if (medico == null)
            {
                return NotFound();
            }

            medico.Usuario.Nombre = Medico.Usuario.Nombre;
            medico.Usuario.Apellido = Medico.Usuario.Apellido;
            medico.Usuario.Telefono = Medico.Usuario.Telefono;
            medico.Usuario.Direccion = Medico.Usuario.Direccion;
            medico.Usuario.Email = Medico.Usuario.Email;
            medico.IdEspecialidad = Medico.IdEspecialidad;
            medico.Usuario.Clave = Medico.Usuario.Clave;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicoExists(Medico.IdMedico))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Medicos");
        }

        private bool MedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.IdMedico == id);
        }
    }
}
