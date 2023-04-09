using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistemafacturacionapi.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class MigracionNumeroSeis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "TblFormaDePago",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "TblFormaDePago");
        }
    }
}
