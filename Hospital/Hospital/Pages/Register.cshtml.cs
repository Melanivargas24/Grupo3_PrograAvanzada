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

            if (NewUser.Email.EndsWith("@ccss.sa.cr", StringComparison.OrdinalIgnoreCase))
            {
                NewUser.Role = "Administrador";
            }
            else
            {
                NewUser.Role = "Empleado";
            }

           

                _context.Usuarios.Add(NewUser);
                _context.SaveChanges();

                if (NewUser.Role == "Administrador")
                {
                    var medico = new Medico
                    {
                        IdUsuario = NewUser.Id,
                        IdEspecialidad = 1
                    };

                    _context.Medicos.Add(medico);
                }
                else
                {
                    var paciente = new Paciente
                    {
                        IdUsuario = NewUser.Id
                    };

                    _context.Pacientes.Add(paciente);
                }

                _context.SaveChanges();

                return RedirectToPage("/Login");
            

         
            }
        }
    }
