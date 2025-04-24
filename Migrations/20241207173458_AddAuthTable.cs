using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestao.Residuos.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NOTIFICACOES");

            migrationBuilder.CreateTable(
                name: "AuthUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    User = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    Role = table.Column<string>(type: "VARCHAR2(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUser", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthUser");

            migrationBuilder.CreateTable(
                name: "NOTIFICACOES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    MoradorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "DATE", nullable: false),
                    Mensagem = table.Column<string>(type: "VARCHAR2(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICACOES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NOTIFICACOES_MORADORES_MoradorId",
                        column: x => x.MoradorId,
                        principalTable: "MORADORES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACOES_MoradorId",
                table: "NOTIFICACOES",
                column: "MoradorId");
        }
    }
}
