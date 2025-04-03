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
            // Cierra la sesi�n del usuario y elimina la cookie de autenticaci�n
            await HttpContext.SignOutAsync();

            // Redirige al usuario a la p�gina de inicio despu�s de cerrar sesi�n
            return RedirectToPage("/Index");
        }
    }
}
