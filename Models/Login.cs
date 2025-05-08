using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Login
    {
        public int Id { get; set; }

        // Clave foránea para Usuario
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Nickname { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\W).{8,64}$",
            ErrorMessage = "La contraseña debe tener emtre 8 y 64 caracteres, una mayúscula y un carácter especial.")]
        public string Password { get; set; }

        // Clave foránea para Estado
        public int EstadoId { get; set; }
        public Estado? Estado { get; set; }

        public int RolId { get; set; }
        public Rol? Rol { get; set; }

    }
}
