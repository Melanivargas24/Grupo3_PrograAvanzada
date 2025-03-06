using CasoPractico.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Pages
{
    public class GameHistoryModel : PageModel
    {
        private readonly SnakeGameContext _context;

        public GameHistoryModel(SnakeGameContext context)
        {
            _context = context;
        }

        public List<GameHistory> GameHistories { get; set; }

        public void OnGet()
        {
            // Obtener las últimas 10 partidas ordenadas por fecha
            GameHistories = _context.GameHistories
                .OrderByDescending(g => g.Score)
                .Take(10)
                .ToList();
        }
    }
}
