using System.ComponentModel.DataAnnotations;
namespace WebApi.Models
{
        public class Estado
        {
            public int Id { get; set; }
            [Required]
            [StringLength(20)]
            public string descripcion { get; set; }
        }
    
}
