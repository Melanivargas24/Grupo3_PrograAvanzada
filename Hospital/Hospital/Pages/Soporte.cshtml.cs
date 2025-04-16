using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace Hospital.Pages
{
    public class SoporteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public SoporteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public SoporteInputModel Soporte { get; set; }

        public bool IsSuccess { get; set; }
        public bool HasError { get; set; }

        public void OnGet()
        {
            IsSuccess = false;
            HasError = false;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fromEmail = _configuration["EmailSettings:From"];
                    var fromPassword = _configuration["EmailSettings:Password"];
                    var toEmail = _configuration["EmailSettings:To"];

                    var fromAddress = new MailAddress(fromEmail, "Soporte Hospital");
                    var toAddress = new MailAddress(toEmail, "Equipo de Soporte");
                    var ccAddress = new MailAddress(Soporte.Email, Soporte.Nombre); // CC al usuario

                    string subject = "Solicitud de Soporte - Sitio Web";
                    string body = $"Nombre: {Soporte.Nombre}\nCorreo: {Soporte.Email}\nMensaje:\n{Soporte.Mensaje}";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                        Timeout = 20000
                    };

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        message.CC.Add(ccAddress); // Copia al usuario
                        smtp.Send(message);
                    }

                    IsSuccess = true;
                }
                catch
                {
                    HasError = true;
                }
            }
            else
            {
                HasError = true;
            }

            return Page();
        }
    }

    public class SoporteInputModel
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Mensaje { get; set; }
    }
}
