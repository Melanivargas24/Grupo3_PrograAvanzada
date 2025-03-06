using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CasoPractico.Models;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace CasoPractico.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly SnakeGameContext _context;

        public CreateUserModel(SnakeGameContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; } = new User();

        [BindProperty]
        [Required(ErrorMessage = "La imagen es obligatoria.")]
        public IFormFile Upload { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Upload != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Upload.CopyToAsync(memoryStream);
                    User.Imagen = memoryStream.ToArray();
                }
            }
            else
            {
                ModelState.AddModelError("Upload", "Debe subir una imagen.");
                return Page();
            }

            if (!string.IsNullOrEmpty(User.Pass))
            {
                User.SetPassword(User.Pass);   
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
