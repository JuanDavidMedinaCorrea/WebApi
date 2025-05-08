using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string apellido { get; set; }
        [Required]
        [StringLength(100)]
        public string email { get; set; }
        [Required]
        [StringLength(100)]
        public string direccion { get; set; }
        [Required]
        [StringLength(100)]
        public string telefono { get; set; }
    }
}
