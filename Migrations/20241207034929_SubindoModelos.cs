using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestao.Residuos.Migrations
{
    /// <inheritdoc />
    public partial class SubindoModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAMINHOES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Placa = table.Column<string>(type: "VARCHAR2(20)", nullable: false),
                    Modelo = table.Column<string>(type: "VARCHAR2(50)", nullable: false),
                    CapacidadeMaxima = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    LocalizacaoAtual = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAMINHOES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MORADORES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    Endereco = table.Column<string>(type: "VARCHAR2(200)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    Telefone = table.Column<string>(type: "VARCHAR2(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MORADORES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RESIDUOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Tipo = table.Column<string>(type: "VARCHAR2(50)", nullable: false),
                    InstrucoesDescarte = table.Column<string>(type: "VARCHAR2(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESIDUOS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ROTAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CaminhaoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataHoraInicio = table.Column<DateTime>(type: "DATE", nullable: false),
                    DataHoraFim = table.Column<DateTime>(type: "DATE", nullable: false),
                    Paradas = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROTAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ROTAS_CAMINHOES_CaminhaoId",
                        column: x => x.CaminhaoId,
                        principalTable: "CAMINHOES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AGENDAMENTOS_COLETAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    MoradorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataColeta = table.Column<DateTime>(type: "DATE", nullable: false),
                    TipoResiduo = table.Column<string>(type: "VARCHAR2(50)", nullable: false),
                    CapacidadeAtualRecipiente = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AGENDAMENTOS_COLETAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AGENDAMENTOS_COLETAS_MORADORES_MoradorId",
                        column: x => x.MoradorId,
                        principalTable: "MORADORES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICACOES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    MoradorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Mensagem = table.Column<string>(type: "VARCHAR2(500)", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "DATE", nullable: false)
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
                name: "IX_AGENDAMENTOS_COLETAS_MoradorId",
                table: "AGENDAMENTOS_COLETAS",
                column: "MoradorId");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACOES_MoradorId",
                table: "NOTIFICACOES",
                column: "MoradorId");

            migrationBuilder.CreateIndex(
                name: "IX_ROTAS_CaminhaoId",
                table: "ROTAS",
                column: "CaminhaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AGENDAMENTOS_COLETAS");

            migrationBuilder.DropTable(
                name: "NOTIFICACOES");

            migrationBuilder.DropTable(
                name: "RESIDUOS");

            migrationBuilder.DropTable(
                name: "ROTAS");

            migrationBuilder.DropTable(
                name: "MORADORES");

            migrationBuilder.DropTable(
                name: "CAMINHOES");
        }
    }
}
