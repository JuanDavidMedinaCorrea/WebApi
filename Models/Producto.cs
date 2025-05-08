using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        [Range(0.01, 1000000)]
        public decimal Precio { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}