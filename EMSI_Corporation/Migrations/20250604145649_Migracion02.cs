using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMSI_Corporation.Migrations
{
    /// <inheritdoc />
    public partial class Migracion02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Trabajador_TrabajadorID",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Trabajador_TrabajadorIdTrabajador",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Trabajador");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_TrabajadorID",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_TrabajadorIdTrabajador",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "TrabajadorIdTrabajador",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Usuario",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "TrabajadorID",
                table: "Usuario",
                newName: "IdEmpleado");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "Usuario",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "usuario",
                table: "Usuario",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomEmpleado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    apeEmpleado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dni = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    gmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    numCelular = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.IdEmpleado);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    IdMenu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomMenu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.IdMenu);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomRol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Rol_Menu",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    IdMenu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol_Menu", x => new { x.IdMenu, x.IdRol });
                    table.ForeignKey(
                        name: "FK_Rol_Menu_Menu_IdMenu",
                        column: x => x.IdMenu,
                        principalTable: "Menu",
                        principalColumn: "IdMenu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rol_Menu_Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Rol",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Rol", x => new { x.IdUsuario, x.IdRol });
                    table.ForeignKey(
                        name: "FK_User_Rol_Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Rol_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdEmpleado",
                table: "Usuario",
                column: "IdEmpleado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rol_Menu_IdRol",
                table: "Rol_Menu",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_User_Rol_IdRol",
                table: "User_Rol",
                column: "IdRol");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Empleado_IdEmpleado",
                table: "Usuario",
                column: "IdEmpleado",
                principalTable: "Empleado",
                principalColumn: "IdEmpleado",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Empleado_IdEmpleado",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Rol_Menu");

            migrationBuilder.DropTable(
                name: "User_Rol");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_IdEmpleado",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "usuario",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Usuario",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "IdEmpleado",
                table: "Usuario",
                newName: "TrabajadorID");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Usuario",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Usuario",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TrabajadorIdTrabajador",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Usuario",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumCelular = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.IdPersona);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    PersonaID = table.Column<int>(type: "int", nullable: false),
                    RUC = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TipoCliente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdCliente);
                    table.ForeignKey(
                        name: "FK_Cliente_Persona_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trabajador",
                columns: table => new
                {
                    IdTrabajador = table.Column<int>(type: "int", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonaID = table.Column<int>(type: "int", nullable: false),
                    Sueldo = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajador", x => x.IdTrabajador);
                    table.ForeignKey(
                        name: "FK_Trabajador_Persona_IdTrabajador",
                        column: x => x.IdTrabajador,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TrabajadorID",
                table: "Usuario",
                column: "TrabajadorID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TrabajadorIdTrabajador",
                table: "Usuario",
                column: "TrabajadorIdTrabajador",
                unique: true,
                filter: "[TrabajadorIdTrabajador] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Trabajador_TrabajadorID",
                table: "Usuario",
                column: "TrabajadorID",
                principalTable: "Trabajador",
                principalColumn: "IdTrabajador",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Trabajador_TrabajadorIdTrabajador",
                table: "Usuario",
                column: "TrabajadorIdTrabajador",
                principalTable: "Trabajador",
                principalColumn: "IdTrabajador");
        }
    }
}
