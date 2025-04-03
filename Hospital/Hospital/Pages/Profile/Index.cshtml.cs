using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace TechCorp.Pages.Profile
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        public string Username { get; private set; }
        public string Role { get; private set; }

        public void OnGet()
        {
            Username = User.Identity.Name;
            Role = User.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}
