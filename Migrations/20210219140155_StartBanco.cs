using Microsoft.EntityFrameworkCore.Migrations;

namespace AnuncioWeb.Migrations
{
    public partial class StartBanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anuncios",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    marca = table.Column<string>(maxLength: 45, nullable: false),
                    modelo = table.Column<string>(maxLength: 45, nullable: false),
                    versao = table.Column<string>(maxLength: 45, nullable: false),
                    ano = table.Column<int>(nullable: false),
                    quilometragem = table.Column<int>(nullable: false),
                    observacao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anuncios", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anuncios");
        }
    }
}
