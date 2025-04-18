using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Migrations
{
    /// <inheritdoc />
    public partial class CambiosCita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__Usuarios__72AFBCC6A9D12EAB",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "UQ__Usuarios__95E042A6D83CF3F2",
                table: "Usuarios");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdPaciente",
                table: "Citas",
                column: "IdPaciente");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Pacientes_IdPaciente",
                table: "Citas",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Pacientes_IdPaciente",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_IdPaciente",
                table: "Citas");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuarios__72AFBCC6A9D12EAB",
                table: "Usuarios",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Usuarios__95E042A6D83CF3F2",
                table: "Usuarios",
                column: "apellido",
                unique: true);
        }
    }
}
