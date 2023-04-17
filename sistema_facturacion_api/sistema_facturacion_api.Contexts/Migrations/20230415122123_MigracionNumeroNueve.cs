using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistemafacturacionapi.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class MigracionNumeroNueve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblDetalleDeFacturas_TblFacturas_FacturasId",
                table: "TblDetalleDeFacturas");

            migrationBuilder.DropIndex(
                name: "IX_TblDetalleDeFacturas_FacturasId",
                table: "TblDetalleDeFacturas");

            migrationBuilder.DropColumn(
                name: "FacturasId",
                table: "TblDetalleDeFacturas");

            migrationBuilder.AddColumn<int>(
                name: "EstadoFacturaId",
                table: "TblFacturas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoFacturaId",
                table: "TblFacturas");

            migrationBuilder.AddColumn<int>(
                name: "FacturasId",
                table: "TblDetalleDeFacturas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblDetalleDeFacturas_FacturasId",
                table: "TblDetalleDeFacturas",
                column: "FacturasId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblDetalleDeFacturas_TblFacturas_FacturasId",
                table: "TblDetalleDeFacturas",
                column: "FacturasId",
                principalTable: "TblFacturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
