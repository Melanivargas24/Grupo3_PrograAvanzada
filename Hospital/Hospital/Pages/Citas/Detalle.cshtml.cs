using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;

namespace Hospital.Pages.Citas
{
    public class DetalleModel : PageModel
    {
        private readonly Hospital.Models.HospitalDBContext _context;

        public DetalleModel(Hospital.Models.HospitalDBContext context)
        {
            _context = context;
        }

        public IList<Cita> Cita { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Cita = await _context.Citas
                .Include(c => c.Especialidad)
                .Include(c => c.Estado)
                .Include(c => c.Medico).ToListAsync();
        }
    }
}
