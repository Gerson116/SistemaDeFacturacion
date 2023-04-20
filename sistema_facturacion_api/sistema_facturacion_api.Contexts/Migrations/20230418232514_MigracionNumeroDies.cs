using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistemafacturacionapi.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class MigracionNumeroDies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblPermiso_TblUsuarios_UsuariosId",
                table: "TblPermiso");

            migrationBuilder.DropIndex(
                name: "IX_TblPermiso_UsuariosId",
                table: "TblPermiso");

            migrationBuilder.DropColumn(
                name: "UsuariosId",
                table: "TblPermiso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuariosId",
                table: "TblPermiso",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblPermiso_UsuariosId",
                table: "TblPermiso",
                column: "UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblPermiso_TblUsuarios_UsuariosId",
                table: "TblPermiso",
                column: "UsuariosId",
                principalTable: "TblUsuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
