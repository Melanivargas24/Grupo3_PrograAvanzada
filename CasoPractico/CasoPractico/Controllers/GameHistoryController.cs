using CasoPractico.Models;
using Microsoft.AspNetCore.Mvc;

namespace CasoPractico.Controllers
{
    [Route("GameHistory")]
    [ApiController]
    public class GameHistoryController : ControllerBase
    {
        private readonly SnakeGameContext _context;

        public GameHistoryController(SnakeGameContext context)
        {
            _context = context;
        }

        // Guardar puntaje cuando el juego termine
        [HttpPost("SaveScore")]
        public IActionResult SaveScore([FromBody] GameHistory gameHistory)
        {
            if (gameHistory == null || string.IsNullOrEmpty(gameHistory.Username))
            {
                return BadRequest("Invalid data.");
            }

            // Asignar fecha del juego
            gameHistory.GameDate = DateTime.Now;

            // Guardar en la base de datos
            _context.GameHistories.Add(gameHistory);
            _context.SaveChanges();

            return Ok(new { message = "Score saved successfully!" });
        }
    }
}
