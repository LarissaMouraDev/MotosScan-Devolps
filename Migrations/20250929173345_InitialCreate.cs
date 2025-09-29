using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotosScan.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Motos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Modelo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Placa = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Localizacao = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UltimoCheckIn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UltimoCheckOut = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ImagemUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motoristas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "TEXT", maxLength: 14, nullable: false),
                    CNH = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    CategoriaCNH = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAdmissao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MotoAtualId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motoristas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motoristas_Motos_MotoAtualId",
                        column: x => x.MotoAtualId,
                        principalTable: "Motos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Manutencoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MotoId = table.Column<int>(type: "INTEGER", nullable: false),
                    MotoristaId = table.Column<int>(type: "INTEGER", nullable: true),
                    TipoManutencao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    DataManutencao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Custo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quilometragem = table.Column<int>(type: "INTEGER", nullable: true),
                    Oficina = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Observacoes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manutencoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manutencoes_Motoristas_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "Motoristas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Manutencoes_Motos_MotoId",
                        column: x => x.MotoId,
                        principalTable: "Motos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manutencoes_MotoId",
                table: "Manutencoes",
                column: "MotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Manutencoes_MotoristaId",
                table: "Manutencoes",
                column: "MotoristaId");

            migrationBuilder.CreateIndex(
                name: "IX_Motoristas_CNH",
                table: "Motoristas",
                column: "CNH",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motoristas_CPF",
                table: "Motoristas",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motoristas_MotoAtualId",
                table: "Motoristas",
                column: "MotoAtualId");

            migrationBuilder.CreateIndex(
                name: "IX_Motos_Placa",
                table: "Motos",
                column: "Placa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Manutencoes");

            migrationBuilder.DropTable(
                name: "Motoristas");

            migrationBuilder.DropTable(
                name: "Motos");
        }
    }
}
