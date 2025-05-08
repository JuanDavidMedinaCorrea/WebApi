using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class primera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logins_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logins_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logins_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Juguetería" },
                    { 2, "Papelería" },
                    { 3, "Mascotas" },
                    { 4, "Salud y Belleza" },
                    { 5, "Videojuegos" },
                    { 6, "Herramientas" }
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "descripcion" },
                values: new object[,]
                {
                    { 1, "Activo" },
                    { 2, "Inactivo" },
                    { 3, "Bloqueado" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "descripcion" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "apellido", "direccion", "email", "nombre", "telefono" },
                values: new object[,]
                {
                    { 1, "Ramírez", "Calle 99 Sur", "lucia.ramirez@correo.com", "Lucía", "3101111122" },
                    { 2, "Moreno", "Carrera 45 Este", "esteban.moreno@correo.com", "Esteban", "3112222233" },
                    { 3, "Ortiz", "Diagonal 12 Norte", "valentina.ortiz@correo.com", "Valentina", "3123333344" },
                    { 4, "Castaño", "Transversal 21B", "sebastian.castano@correo.com", "Sebastián", "3134444455" },
                    { 5, "Mejía", "Calle 78A", "daniela.mejia@correo.com", "Daniela", "3145555566" },
                    { 6, "Vargas", "Avenida Central 15", "camilo.vargas@correo.com", "Camilo", "3156666677" }
                });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EstadoId", "Nickname", "Password", "RolId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 1, "LuciaR99", "Lucia@2024", 1, 1 },
                    { 2, 1, "EstebanMo", "Esteban@2024", 2, 2 },
                    { 3, 1, "ValeOrtiz", "Vale@2024", 2, 3 },
                    { 4, 1, "SebasC21", "Sebas@2024", 2, 4 },
                    { 5, 1, "Daniela78", "Dani@2024", 2, 5 },
                    { 6, 1, "CamVargas", "Camilo@2024", 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "CategoriaId", "Descripcion", "Nombre", "Precio", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Juego de construcción para niños de 3 años en adelante", "Set de Bloques", 45.99m, 25 },
                    { 2, 2, "Agenda con calendario académico y diseño juvenil", "Agenda Escolar", 18.50m, 40 },
                    { 3, 3, "Bolsa de 10kg para perros adultos sabor carne", "Alimento Canino", 65.00m, 15 },
                    { 4, 4, "Crema hidratante con vitamina E para todo tipo de piel", "Crema Facial", 22.99m, 30 },
                    { 5, 5, "Control compatible con Xbox y PC", "Control Inalámbrico", 59.90m, 12 },
                    { 6, 6, "Taladro de 500W con múltiples velocidades", "Taladro Eléctrico", 89.75m, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logins_EstadoId",
                table: "Logins",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_RolId",
                table: "Logins",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_UsuarioId",
                table: "Logins",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
