using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Datos;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);

            // Registrar en el log
            var log = new CategoriaLog
            {
                CategoriaIdEliminada = id,
                FechaEliminacion = DateTime.UtcNow
            };
            _context.CategoriaLogs.Add(log);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("log/buscar/{idCategoria}")]
        public async Task<IActionResult> BuscarLog(int idCategoria)
        {
            var log = await _context.CategoriaLogs
                .Where(l => l.CategoriaIdEliminada == idCategoria)
                .ToListAsync();

            if (!log.Any())
                return NotFound();

            return Ok(log);
        }
    }
}
