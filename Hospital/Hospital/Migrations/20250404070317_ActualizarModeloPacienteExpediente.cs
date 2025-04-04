using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarModeloPacienteExpediente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_Pacientes_PacienteId1",
                table: "Expedientes");

            migrationBuilder.DropIndex(
                name: "IX_Expedientes_PacienteId1",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "PacienteId1",
                table: "Expedientes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacienteId1",
                table: "Expedientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_PacienteId1",
                table: "Expedientes",
                column: "PacienteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Pacientes_PacienteId1",
                table: "Expedientes",
                column: "PacienteId1",
                principalTable: "Pacientes",
                principalColumn: "Id");
        }
    }
}
