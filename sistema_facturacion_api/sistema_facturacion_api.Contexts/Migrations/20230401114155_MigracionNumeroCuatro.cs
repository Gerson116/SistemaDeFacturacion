using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistemafacturacionapi.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class MigracionNumeroCuatro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblEmpresas_TblUsuarios_UsuariosId",
                table: "TblEmpresas");

            migrationBuilder.DropIndex(
                name: "IX_TblEmpresas_UsuariosId",
                table: "TblEmpresas");

            migrationBuilder.DropColumn(
                name: "UsuariosId",
                table: "TblEmpresas");

            migrationBuilder.AddColumn<int>(
                name: "TblUsuariosId",
                table: "TblEmpresas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblEmpresas_TblUsuariosId",
                table: "TblEmpresas",
                column: "TblUsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblEmpresas_TblUsuarios_TblUsuariosId",
                table: "TblEmpresas",
                column: "TblUsuariosId",
                principalTable: "TblUsuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblEmpresas_TblUsuarios_TblUsuariosId",
                table: "TblEmpresas");

            migrationBuilder.DropIndex(
                name: "IX_TblEmpresas_TblUsuariosId",
                table: "TblEmpresas");

            migrationBuilder.DropColumn(
                name: "TblUsuariosId",
                table: "TblEmpresas");

            migrationBuilder.AddColumn<int>(
                name: "UsuariosId",
                table: "TblEmpresas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblEmpresas_UsuariosId",
                table: "TblEmpresas",
                column: "UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblEmpresas_TblUsuarios_UsuariosId",
                table: "TblEmpresas",
                column: "UsuariosId",
                principalTable: "TblUsuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
