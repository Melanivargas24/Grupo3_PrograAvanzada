using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Migrations
{
    /// <inheritdoc />
    public partial class CambiosUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdDireccion",
                table: "Medicos",
                newName: "IdUsuario");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_IdUsuario",
                table: "Pacientes",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_IdUsuario",
                table: "Medicos",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_Usuarios_IdUsuario",
                table: "Medicos",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Usuarios_IdUsuario",
                table: "Pacientes",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Usuarios_IdUsuario",
                table: "Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Usuarios_IdUsuario",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_IdUsuario",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Medicos_IdUsuario",
                table: "Medicos");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Medicos",
                newName: "IdDireccion");
        }
    }
}
