using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Hospital.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            // Cierra la sesión del usuario y elimina la cookie de autenticación
            await HttpContext.SignOutAsync();

            // Redirige al usuario a la página de inicio después de cerrar sesión
            return RedirectToPage("/Index");
        }
    }
}
