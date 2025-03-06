using Microsoft.AspNetCore.Mvc;
using CasoPractico.Models;
using static System.Net.Mime.MediaTypeNames;

namespace CasoPractico.Controllers;

public class ImagenController : Controller
{
    private readonly SnakeGameContext _context;

    public ImagenController(SnakeGameContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> nuevoUsuario([Bind("Nombre")] User user, IFormFile upload)
    {
        try
        {
            if (ModelState.IsValid && upload != null && upload.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await upload.CopyToAsync(memoryStream);
                    user.Imagen = memoryStream.ToArray();
                }
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index"); 
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Error al guardar la imagen. Intenta nuevamente.");
        }

        return View(user);
    }


    public IActionResult convertirImagen(int codigo)
    {
        var user = _context.Users
            .FirstOrDefault(a => a.Id == codigo);

        if (user != null && user.Imagen != null)
        {
            return File(user.Imagen, "image/jpeg");
        }
        return NotFound();
    }
}