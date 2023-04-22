using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistemafacturacionapi.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "TblProductos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "TblMarca",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "TblProductos");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "TblMarca");
        }
    }
}
