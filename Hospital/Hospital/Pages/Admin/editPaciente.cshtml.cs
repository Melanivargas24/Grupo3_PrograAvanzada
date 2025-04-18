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
    public class editModel : PageModel
    {
        private readonly Hospital.Models.HospitalDBContext _context;

        public editModel(Hospital.Models.HospitalDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Paciente Paciente { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
              .Include(p => p.Usuario)
              .FirstOrDefaultAsync(m => m.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }
            Paciente = paciente;
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

            // Buscar el paciente actual incluyendo el Usuario
            var paciente = await _context.Pacientes
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.Id == Paciente.Id);

            if (paciente == null)
            {
                return NotFound();
            }

            paciente.Usuario.Nombre = Paciente.Usuario.Nombre;
            paciente.Usuario.Apellido = Paciente.Usuario.Apellido;
            paciente.Usuario.Telefono = Paciente.Usuario.Telefono;
            paciente.Usuario.Direccion = Paciente.Usuario.Direccion;
            paciente.Usuario.Email = Paciente.Usuario.Email;
            paciente.Usuario.Clave = Paciente.Usuario.Clave;

    

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(Paciente.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }


    }
}
