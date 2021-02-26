using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnuncioWeb.Migrations
{
    public partial class AtualizacaoBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Anuncios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Atualizado",
                table: "Anuncios",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Criado",
                table: "Anuncios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Anuncios");

            migrationBuilder.DropColumn(
                name: "Atualizado",
                table: "Anuncios");

            migrationBuilder.DropColumn(
                name: "Criado",
                table: "Anuncios");
        }
    }
}
