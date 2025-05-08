using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Datos;
using WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text;

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


        [HttpGet("buscar-producto-nombre")]
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


        [HttpDelete("borrar-producto-id")]
        public async Task<IActionResult> EliminarProducto([FromQuery] int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound($"No hay productos con ese id {id}.");
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Producto que se elimino: id={producto.Id}, nombre={producto.Nombre}");

            return Ok(new { mensaje = "Producto eliminado correctamente", producto.Id, producto.Nombre });
        }


        [HttpGet("buscar-producto-descripcion")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> BuscarPorDescripcion([FromQuery] string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                return BadRequest("La descripción no puede estar vacía.");
            }

            var descripcionNormalizada = RemoverAcentos(descripcion.ToLower());

            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .ToListAsync();

            var productosFiltrados = productos
                .Where(p =>
                    RemoverAcentos(p.Descripcion.ToLower())
                    .Contains(descripcionNormalizada))
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
                .ToList();

            if (productosFiltrados.Count == 0)
            {
                return NotFound("No se encontraron productos con esa descripción.");
            }

            return Ok(productosFiltrados);
        }

        private string RemoverAcentos(string texto)
        {
            var normalized = texto.Normalize(System.Text.NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }



    }
}

