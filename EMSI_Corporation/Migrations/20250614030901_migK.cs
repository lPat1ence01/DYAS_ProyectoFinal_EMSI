﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMSI_Corporation.Migrations
{
    /// <inheritdoc />
    public partial class migK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomCliente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoCliente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumDocumento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumCelular = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "ComprobantesServicio",
                columns: table => new
                {
                    IdComprobante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoServicio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprobantesServicio", x => x.IdComprobante);
                });

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
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    IdVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Cliente_ID = table.Column<int>(type: "int", nullable: false),
                    Empleado_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.IdVenta);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_Cliente_ID",
                        column: x => x.Cliente_ID,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_Empleado_Empleado_ID",
                        column: x => x.Empleado_ID,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitasTecnicas",
                columns: table => new
                {
                    IdVisita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empleado_ID = table.Column<int>(type: "int", nullable: false),
                    Cliente_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitasTecnicas", x => x.IdVisita);
                    table.ForeignKey(
                        name: "FK_VisitasTecnicas_Clientes_Cliente_ID",
                        column: x => x.Cliente_ID,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitasTecnicas_Empleado_Empleado_ID",
                        column: x => x.Empleado_ID,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Comprobantes",
                columns: table => new
                {
                    IdComprobante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoComprobante = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NroComprobante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Venta_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comprobantes", x => x.IdComprobante);
                    table.ForeignKey(
                        name: "FK_Comprobantes_Ventas_Venta_ID",
                        column: x => x.Venta_ID,
                        principalTable: "Ventas",
                        principalColumn: "IdVenta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Extintores",
                columns: table => new
                {
                    IdExtintor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoAgente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClaseFuego = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CapacidadKG = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Venta_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extintores", x => x.IdExtintor);
                    table.ForeignKey(
                        name: "FK_Extintores_Ventas_Venta_ID",
                        column: x => x.Venta_ID,
                        principalTable: "Ventas",
                        principalColumn: "IdVenta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InformesTecnicos",
                columns: table => new
                {
                    IdInforme = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInforme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Informe_PDF = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Visita_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformesTecnicos", x => x.IdInforme);
                    table.ForeignKey(
                        name: "FK_InformesTecnicos_VisitasTecnicas_Visita_ID",
                        column: x => x.Visita_ID,
                        principalTable: "VisitasTecnicas",
                        principalColumn: "IdVisita",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    IdMantenimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaMantenimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LugarAdecuado = table.Column<bool>(type: "bit", nullable: false),
                    Señalización = table.Column<bool>(type: "bit", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    Usado = table.Column<bool>(type: "bit", nullable: false),
                    EstadoPrecinto = table.Column<bool>(type: "bit", nullable: false),
                    EstadoIndicador = table.Column<bool>(type: "bit", nullable: false),
                    PresionCorrecta = table.Column<bool>(type: "bit", nullable: false),
                    ExteriorCorrecto = table.Column<bool>(type: "bit", nullable: false),
                    PesoCorrecto = table.Column<bool>(type: "bit", nullable: false),
                    MangueraCorrecta = table.Column<bool>(type: "bit", nullable: false),
                    BoquillaCorrecta = table.Column<bool>(type: "bit", nullable: false),
                    InstruccionesVisibles = table.Column<bool>(type: "bit", nullable: false),
                    AperturaCorrecta = table.Column<bool>(type: "bit", nullable: false),
                    BarometroCorrecto = table.Column<bool>(type: "bit", nullable: false),
                    Empleado_ID = table.Column<int>(type: "int", nullable: false),
                    Extintor_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimientos", x => x.IdMantenimiento);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Empleado_Empleado_ID",
                        column: x => x.Empleado_ID,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Extintores_Extintor_ID",
                        column: x => x.Extintor_ID,
                        principalTable: "Extintores",
                        principalColumn: "IdExtintor");
                });

            migrationBuilder.CreateTable(
                name: "Recargas",
                columns: table => new
                {
                    IdRecarga = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRecarga = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaterialUsado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CantidadKG = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PresionPostRecarga = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Extintor_ID = table.Column<int>(type: "int", nullable: false),
                    Empleado_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recargas", x => x.IdRecarga);
                    table.ForeignKey(
                        name: "FK_Recargas_Empleado_Empleado_ID",
                        column: x => x.Empleado_ID,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recargas_Extintores_Extintor_ID",
                        column: x => x.Extintor_ID,
                        principalTable: "Extintores",
                        principalColumn: "IdExtintor");
                });

            migrationBuilder.CreateTable(
                name: "ReportesServicio",
                columns: table => new
                {
                    IdReporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaCliente = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgEvidencia = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false),
                    Cliente_ID = table.Column<int>(type: "int", nullable: false),
                    Comprobante_ID = table.Column<int>(type: "int", nullable: false),
                    Mantenimiento_ID = table.Column<int>(type: "int", nullable: false),
                    Recarga_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportesServicio", x => x.IdReporte);
                    table.ForeignKey(
                        name: "FK_ReportesServicio_Clientes_Cliente_ID",
                        column: x => x.Cliente_ID,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportesServicio_ComprobantesServicio_Comprobante_ID",
                        column: x => x.Comprobante_ID,
                        principalTable: "ComprobantesServicio",
                        principalColumn: "IdComprobante",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportesServicio_Mantenimientos_Mantenimiento_ID",
                        column: x => x.Mantenimiento_ID,
                        principalTable: "Mantenimientos",
                        principalColumn: "IdMantenimiento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportesServicio_Recargas_Recarga_ID",
                        column: x => x.Recarga_ID,
                        principalTable: "Recargas",
                        principalColumn: "IdRecarga");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comprobantes_Venta_ID",
                table: "Comprobantes",
                column: "Venta_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Extintores_Venta_ID",
                table: "Extintores",
                column: "Venta_ID");

            migrationBuilder.CreateIndex(
                name: "IX_InformesTecnicos_Visita_ID",
                table: "InformesTecnicos",
                column: "Visita_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_Empleado_ID",
                table: "Mantenimientos",
                column: "Empleado_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_Extintor_ID",
                table: "Mantenimientos",
                column: "Extintor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Recargas_Empleado_ID",
                table: "Recargas",
                column: "Empleado_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Recargas_Extintor_ID",
                table: "Recargas",
                column: "Extintor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesServicio_Cliente_ID",
                table: "ReportesServicio",
                column: "Cliente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesServicio_Comprobante_ID",
                table: "ReportesServicio",
                column: "Comprobante_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesServicio_Mantenimiento_ID",
                table: "ReportesServicio",
                column: "Mantenimiento_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesServicio_Recarga_ID",
                table: "ReportesServicio",
                column: "Recarga_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_Menu_IdRol",
                table: "Rol_Menu",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_User_Rol_IdRol",
                table: "User_Rol",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdEmpleado",
                table: "Usuario",
                column: "IdEmpleado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_Cliente_ID",
                table: "Ventas",
                column: "Cliente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_Empleado_ID",
                table: "Ventas",
                column: "Empleado_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VisitasTecnicas_Cliente_ID",
                table: "VisitasTecnicas",
                column: "Cliente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VisitasTecnicas_Empleado_ID",
                table: "VisitasTecnicas",
                column: "Empleado_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comprobantes");

            migrationBuilder.DropTable(
                name: "InformesTecnicos");

            migrationBuilder.DropTable(
                name: "ReportesServicio");

            migrationBuilder.DropTable(
                name: "Rol_Menu");

            migrationBuilder.DropTable(
                name: "User_Rol");

            migrationBuilder.DropTable(
                name: "VisitasTecnicas");

            migrationBuilder.DropTable(
                name: "ComprobantesServicio");

            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "Recargas");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Extintores");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Empleado");
        }
    }
}
