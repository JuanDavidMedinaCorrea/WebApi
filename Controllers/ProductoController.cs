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
    }
}