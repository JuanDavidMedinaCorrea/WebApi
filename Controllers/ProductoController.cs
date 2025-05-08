using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Datos;
using WebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    Categoria = new CategoriaDto
                    {
                        Id = p.Categoria.Id,
                        Nombre = p.Categoria.Nombre
                    }
                })
                .ToListAsync();
        }
        [HttpGet("filtrar-por-stock")]
        public async Task<IActionResult> FiltrarPorStock([FromQuery] bool enStock)
        {
            var productos = enStock
                ? await _context.Productos.Where(p => p.Stock > 0).ToListAsync()
                : await _context.Productos.Where(p => p.Stock == 0).ToListAsync();

            return Ok(productos);
        }


        [HttpGet("buscar")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> BuscarProductosPorNombre([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest("El nombre no puede estar vacío.");
            }

            var productos = await _context.Productos
                .Where(p => p.Nombre.ToLower().Contains(nombre.ToLower()))
                .Include(p => p.Categoria)
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    Categoria = new CategoriaDto
                    {
                        Id = p.Categoria.Id,
                        Nombre = p.Categoria.Nombre
                    }
                })
                .ToListAsync();

            if (productos.Count == 0)
            {
                return NotFound("No se encontraron productos con ese nombre.");
            }

            return Ok(productos);
        }


    }
}