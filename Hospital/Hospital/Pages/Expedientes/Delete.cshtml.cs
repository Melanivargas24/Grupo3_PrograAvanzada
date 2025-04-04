using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public Expediente Expediente { get; set; }

        public IActionResult OnGet(int id)
        {
            Expediente = _context.Expedientes.FirstOrDefault(e => e.Id == id);

            if (Expediente == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            var expediente = _context.Expedientes.FirstOrDefault(e => e.Id == Expediente.Id);

            if (expediente == null)
            {
                return NotFound();
            }

            _context.Expedientes.Remove(expediente);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}