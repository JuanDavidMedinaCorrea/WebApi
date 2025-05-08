using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        //Santiago
        // Relación con productos
        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}
