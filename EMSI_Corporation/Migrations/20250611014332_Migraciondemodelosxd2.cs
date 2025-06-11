using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMSI_Corporation.Migrations
{
    /// <inheritdoc />
    public partial class Migraciondemodelosxd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mantenimientos_Extintores_Extintor_ID",
                table: "Mantenimientos");

            migrationBuilder.AddForeignKey(
                name: "FK_Mantenimientos_Extintores_Extintor_ID",
                table: "Mantenimientos",
                column: "Extintor_ID",
                principalTable: "Extintores",
                principalColumn: "IdExtintor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mantenimientos_Extintores_Extintor_ID",
                table: "Mantenimientos");

            migrationBuilder.AddForeignKey(
                name: "FK_Mantenimientos_Extintores_Extintor_ID",
                table: "Mantenimientos",
                column: "Extintor_ID",
                principalTable: "Extintores",
                principalColumn: "IdExtintor",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
