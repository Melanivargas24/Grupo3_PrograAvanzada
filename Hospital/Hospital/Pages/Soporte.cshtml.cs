// Pages/Soporte.cshtml.cs
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hospital.Pages
{
    public class SoporteModel : PageModel
    {
        [BindProperty]
        public SoporteModel Soporte { get; set; }

        public bool IsSuccess { get; set; }
        public bool HasError { get; set; }

        public void OnGet()
        {
            // Inicializamos las propiedades por si la p�gina se carga sin un mensaje de �xito o error.
            IsSuccess = false;
            HasError = false;
        }

        public IActionResult OnPost()
        {
            // Validaci�n de los datos
            if (ModelState.IsValid)
            {
                // Simular enviar el mensaje (esto puede ser reemplazado por un servicio real de correo electr�nico o base de datos)
                // Aqu� podr�as agregar c�digo para enviar el mensaje por correo electr�nico, almacenarlo en la base de datos, etc.

                // Si todo sale bien, mostramos el mensaje de �xito
                IsSuccess = true;
                return Page();
            }

            // Si algo sali� mal, mostramos un mensaje de error
            HasError = true;
            return Page();
        }
    }
}
