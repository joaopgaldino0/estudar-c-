using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTeste.Migrations
{
    /// <inheritdoc />
    public partial class AddEmprestadoToLivro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Emprestado",
                table: "Livros",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Emprestado",
                table: "Livros");
        }
    }
}
