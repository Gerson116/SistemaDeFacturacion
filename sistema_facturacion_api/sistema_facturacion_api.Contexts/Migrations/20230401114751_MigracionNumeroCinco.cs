using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistemafacturacionapi.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class MigracionNumeroCinco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblEmpresas_TblUsuarios_TblUsuariosId",
                table: "TblEmpresas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TblEmpresas");

            migrationBuilder.AlterColumn<int>(
                name: "TblUsuariosId",
                table: "TblEmpresas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TblEmpresas_TblUsuarios_TblUsuariosId",
                table: "TblEmpresas",
                column: "TblUsuariosId",
                principalTable: "TblUsuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblEmpresas_TblUsuarios_TblUsuariosId",
                table: "TblEmpresas");

            migrationBuilder.AlterColumn<int>(
                name: "TblUsuariosId",
                table: "TblEmpresas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "TblEmpresas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TblEmpresas_TblUsuarios_TblUsuariosId",
                table: "TblEmpresas",
                column: "TblUsuariosId",
                principalTable: "TblUsuarios",
                principalColumn: "Id");
        }
    }
}
