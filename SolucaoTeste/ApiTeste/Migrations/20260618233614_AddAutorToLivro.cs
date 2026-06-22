using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTeste.Migrations
{
    /// <inheritdoc />
    public partial class AddAutorToLivro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Livros");

            migrationBuilder.AddColumn<string>(
                name: "Autor",
                table: "Livros",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autor",
                table: "Livros");

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Livros",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
