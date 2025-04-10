using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Pages.Expedientes
{
    public class CrearModel : PageModel
{
    private readonly HospitalDBContext _context;

    public CrearModel(HospitalDBContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Expediente Expediente { get; set; }

    public SelectList PacientesSelectList { get; set; } = null!;

    public void OnGet()
    {
        var pacientes = _context.Pacientes
            .Select(p => new PacienteSelectItem
            {
                Id = p.Id,
                NombreCompleto = p.Nombre + " " + p.Apellido
            })
            .ToList();

        PacientesSelectList = new SelectList(pacientes, "Id", "NombreCompleto");
    }

    public IActionResult OnPost()
    {
        Console.WriteLine(">>> OnPost ejecutado");
        Console.WriteLine($"PacienteId recibido: {Expediente.PacienteId}");
        Console.WriteLine($"ModelState válido: {ModelState.IsValid}");

        if (!ModelState.IsValid)
        {
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Error en campo '{state.Key}': {error.ErrorMessage}");
                }
            }

            var pacientes = _context.Pacientes
                .Select(p => new PacienteSelectItem
                {
                    Id = p.Id,
                    NombreCompleto = p.Nombre + " " + p.Apellido
                })
                .ToList();

            PacientesSelectList = new SelectList(pacientes, "Id", "NombreCompleto");

            return Page();
        }

        Expediente.FechaCreacion = DateTime.Now;
        _context.Expedientes.Add(Expediente);
        _context.SaveChanges();

        return RedirectToPage("/Expedientes/Index");
    }

    private class PacienteSelectItem
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
}