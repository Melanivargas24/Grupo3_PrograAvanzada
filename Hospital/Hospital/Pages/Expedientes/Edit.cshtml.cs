using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Pages.Expedientes
{
    public class EditModel : PageModel
    {
        private readonly HospitalDBContext _context;

        public EditModel(HospitalDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Expediente Expediente { get; set; } = null!;

        public SelectList PacientesSelectList { get; set; } = null!;

        public string NombrePaciente { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            PacientesSelectList = new SelectList(
                _context.Pacientes
                    .Include(p => p.Usuario)
                    .Select(p => new
                    {
                        Id = p.Id,
                        NombreCompleto = p.Usuario.Nombre + " " + p.Usuario.Apellido
                    }),
                "Id", "NombreCompleto");

            Expediente = await _context.Expedientes.FindAsync(id);

            if (Expediente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.Id == Expediente.PacienteId);

            NombrePaciente = paciente != null ? $"{paciente.Usuario.Nombre} {paciente.Usuario.Apellido}" : "Paciente no encontrado";

            // Obtener historial de citas
            var citas = await _context.Citas
                .Where(c => c.IdPaciente == Expediente.PacienteId)
                .Include(c => c.Medico).ThenInclude(m => m.Usuario)
                .Include(c => c.Especialidad)
                .OrderByDescending(c => c.Fecha)
                .ToListAsync();

            var historial = citas.Select(c =>
                $"- {c.Fecha.ToShortDateString()} con Dr. {c.Medico.Usuario.Nombre} {c.Medico.Usuario.Apellido} ({c.Especialidad.Nombre})");

            Expediente.Historial = string.Join("\n", historial);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                PacientesSelectList = new SelectList(
                    _context.Pacientes
                        .Include(p => p.Usuario)
                        .Select(p => new
                        {
                            Id = p.Id,
                            NombreCompleto = p.Usuario.Nombre + " " + p.Usuario.Apellido
                        }),
                    "Id", "NombreCompleto");

                return Page();
            }

            var expedienteExistente = _context.Expedientes.FirstOrDefault(e => e.Id == Expediente.Id);
            if (expedienteExistente == null)
            {
                return NotFound();
            }

            expedienteExistente.Medicamentos = Expediente.Medicamentos;
            expedienteExistente.Tratamientos = Expediente.Tratamientos;
            expedienteExistente.Historial = Expediente.Historial;
            expedienteExistente.Diagnostico = Expediente.Diagnostico;
            expedienteExistente.Fecha = Expediente.Fecha;

            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}