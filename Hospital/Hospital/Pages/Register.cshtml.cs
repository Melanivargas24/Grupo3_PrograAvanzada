using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hospital.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly HospitalDBContext _context;

        public RegisterModel(HospitalDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario NewUser { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Usuarios.Add(NewUser);
            _context.SaveChanges();

            return RedirectToPage("/Login");
        }
    }
}
