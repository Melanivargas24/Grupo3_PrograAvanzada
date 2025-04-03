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
            // Inicializamos las propiedades por si la página se carga sin un mensaje de éxito o error.
            IsSuccess = false;
            HasError = false;
        }

        public IActionResult OnPost()
        {
            // Validación de los datos
            if (ModelState.IsValid)
            {
                // Simular enviar el mensaje (esto puede ser reemplazado por un servicio real de correo electrónico o base de datos)
                // Aquí podrías agregar código para enviar el mensaje por correo electrónico, almacenarlo en la base de datos, etc.

                // Si todo sale bien, mostramos el mensaje de éxito
                IsSuccess = true;
                return Page();
            }

            // Si algo salió mal, mostramos un mensaje de error
            HasError = true;
            return Page();
        }
    }
}
