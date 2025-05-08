using Microsoft.EntityFrameworkCore;
using WebApi.Models;
namespace WebApi.Datos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
           .Property(p => p.Precio)
           .HasPrecision(18, 2);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId);

            modelBuilder.Entity<Estado>().HasData(
                new Estado { Id = 1, descripcion = "Activo" },
                new Estado { Id = 2, descripcion = "Inactivo" },
                new Estado { Id = 3, descripcion = "Bloqueado" }
            );
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, descripcion = "Administrador" },
                new Rol { Id = 2, descripcion = "Cliente" }
            );
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, nombre = "Lucía", apellido = "Ramírez", email = "lucia.ramirez@correo.com", direccion = "Calle 99 Sur", telefono = "3101111122" },
                new Usuario { Id = 2, nombre = "Esteban", apellido = "Moreno", email = "esteban.moreno@correo.com", direccion = "Carrera 45 Este", telefono = "3112222233" },
                new Usuario { Id = 3, nombre = "Valentina", apellido = "Ortiz", email = "valentina.ortiz@correo.com", direccion = "Diagonal 12 Norte", telefono = "3123333344" },
                new Usuario { Id = 4, nombre = "Sebastián", apellido = "Castaño", email = "sebastian.castano@correo.com", direccion = "Transversal 21B", telefono = "3134444455" },
                new Usuario { Id = 5, nombre = "Daniela", apellido = "Mejía", email = "daniela.mejia@correo.com", direccion = "Calle 78A", telefono = "3145555566" },
                new Usuario { Id = 6, nombre = "Camilo", apellido = "Vargas", email = "camilo.vargas@correo.com", direccion = "Avenida Central 15", telefono = "3156666677" }
            );

            modelBuilder.Entity<Login>().HasData(
                new Login { Id = 1, UsuarioId = 1, Nickname = "LuciaR99", Password = "Lucia@2024", EstadoId = 1, RolId = 1 },
                new Login { Id = 2, UsuarioId = 2, Nickname = "EstebanMo", Password = "Esteban@2024", EstadoId = 1, RolId = 2 },
                new Login { Id = 3, UsuarioId = 3, Nickname = "ValeOrtiz", Password = "Vale@2024", EstadoId = 1, RolId = 2 },
                new Login { Id = 4, UsuarioId = 4, Nickname = "SebasC21", Password = "Sebas@2024", EstadoId = 1, RolId = 2 },
                new Login { Id = 5, UsuarioId = 5, Nickname = "Daniela78", Password = "Dani@2024", EstadoId = 1, RolId = 2 },
                new Login { Id = 6, UsuarioId = 6, Nickname = "CamVargas", Password = "Camilo@2024", EstadoId = 1, RolId = 2 }
            );

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Juguetería" },
                new Categoria { Id = 2, Nombre = "Papelería" },
                new Categoria { Id = 3, Nombre = "Mascotas" },
                new Categoria { Id = 4, Nombre = "Salud y Belleza" },
                new Categoria { Id = 5, Nombre = "Videojuegos" },
                new Categoria { Id = 6, Nombre = "Herramientas" }
            );


            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Set de Bloques", Descripcion = "Juego de construcción para niños de 3 años en adelante", Precio = 45.99M, Stock = 25, CategoriaId = 1 },
                new Producto { Id = 2, Nombre = "Agenda Escolar", Descripcion = "Agenda con calendario académico y diseño juvenil", Precio = 18.50M, Stock = 40, CategoriaId = 2 },
                new Producto { Id = 3, Nombre = "Alimento Canino", Descripcion = "Bolsa de 10kg para perros adultos sabor carne", Precio = 65.00M, Stock = 15, CategoriaId = 3 },
                new Producto { Id = 4, Nombre = "Crema Facial", Descripcion = "Crema hidratante con vitamina E para todo tipo de piel", Precio = 22.99M, Stock = 30, CategoriaId = 4 },
                new Producto { Id = 5, Nombre = "Control Inalámbrico", Descripcion = "Control compatible con Xbox y PC", Precio = 59.90M, Stock = 12, CategoriaId = 5 },
                new Producto { Id = 6, Nombre = "Taladro Eléctrico", Descripcion = "Taladro de 500W con múltiples velocidades", Precio = 89.75M, Stock = 20, CategoriaId = 6 }
            );

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

    }
}
