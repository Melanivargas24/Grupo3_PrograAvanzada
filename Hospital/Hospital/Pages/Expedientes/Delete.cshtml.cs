using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Pages.Expedientes
{
    public class DeleteModel : PageModel
    {
        private readonly HospitalDBContext _context;

        public DeleteModel(HospitalDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Expediente Expediente { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Expediente = await _context.Expedientes
             .Include(e => e.Paciente)
            .ThenInclude(p => p.Usuario) 
             .FirstOrDefaultAsync(m => m.Id == id);


            if (Expediente == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.Expedientes.FindAsync(id);

            if (expediente != null)
            {
                _context.Expedientes.Remove(expediente);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}