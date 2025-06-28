using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMSI_Corporation.Migrations
{
    /// <inheritdoc />
    public partial class Proovedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extintores_Ventas_Venta_ID",
                table: "Extintores");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_IdEmpleado",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "IdEmpleado",
                table: "Usuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Venta_ID",
                table: "Extintores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RecepcionId",
                table: "Extintores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Proovedor",
                columns: table => new
                {
                    IdProovedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RUC = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NumCelular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proovedor", x => x.IdProovedor);
                });

            migrationBuilder.CreateTable(
                name: "Recepcion",
                columns: table => new
                {
                    IdRecepcion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    ProovedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepcion", x => x.IdRecepcion);
                    table.ForeignKey(
                        name: "FK_Recepcion_Proovedor_ProovedorId",
                        column: x => x.ProovedorId,
                        principalTable: "Proovedor",
                        principalColumn: "IdProovedor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recepcion_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdEmpleado",
                table: "Usuario",
                column: "IdEmpleado",
                unique: true,
                filter: "[IdEmpleado] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Extintores_RecepcionId",
                table: "Extintores",
                column: "RecepcionId");

            migrationBuilder.CreateIndex(
                name: "IX_Recepcion_ProovedorId",
                table: "Recepcion",
                column: "ProovedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Recepcion_UsuarioId",
                table: "Recepcion",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extintores_Recepcion_RecepcionId",
                table: "Extintores",
                column: "RecepcionId",
                principalTable: "Recepcion",
                principalColumn: "IdRecepcion");

            migrationBuilder.AddForeignKey(
                name: "FK_Extintores_Ventas_Venta_ID",
                table: "Extintores",
                column: "Venta_ID",
                principalTable: "Ventas",
                principalColumn: "IdVenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extintores_Recepcion_RecepcionId",
                table: "Extintores");

            migrationBuilder.DropForeignKey(
                name: "FK_Extintores_Ventas_Venta_ID",
                table: "Extintores");

            migrationBuilder.DropTable(
                name: "Recepcion");

            migrationBuilder.DropTable(
                name: "Proovedor");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_IdEmpleado",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Extintores_RecepcionId",
                table: "Extintores");

            migrationBuilder.DropColumn(
                name: "RecepcionId",
                table: "Extintores");

            migrationBuilder.AlterColumn<int>(
                name: "IdEmpleado",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Venta_ID",
                table: "Extintores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdEmpleado",
                table: "Usuario",
                column: "IdEmpleado",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Extintores_Ventas_Venta_ID",
                table: "Extintores",
                column: "Venta_ID",
                principalTable: "Ventas",
                principalColumn: "IdVenta",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
