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

        // Endpoint básico: devuelve todos los productos sin ordenamiento
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

        // Endpoint adicional con ruta distinta: permite ordenar por Id
        [HttpGet("ordenados")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductosOrdenados([FromQuery] string orden = "asc")
        {
            var query = _context.Productos
                .Include(p => p.Categoria)
                .AsQueryable();

            query = orden.ToLower() == "desc"
                ? query.OrderByDescending(p => p.Id)
                : query.OrderBy(p => p.Id);

            var productos = await query
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

            return Ok(productos);
        }

        // Nuevo endpoint: lista de productos ordenada ascendentemente por Id
        [HttpGet("ordenadosAsc")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductosOrdenadosAsc()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .OrderBy(p => p.Id)
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

            return Ok(productos);
        }
    }
}
