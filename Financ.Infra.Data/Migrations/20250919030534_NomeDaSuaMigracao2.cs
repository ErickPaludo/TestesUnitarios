using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Financ.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaSuaMigracao2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fnc_contas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TipoConta = table.Column<int>(type: "INTEGER", nullable: false),
                    DiaFechamento = table.Column<int>(type: "INTEGER", maxLength: 16, precision: 2, scale: 0, nullable: false),
                    DiaVencimento = table.Column<int>(type: "INTEGER", maxLength: 12, precision: 2, scale: 0, nullable: false),
                    ContasUsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    DthrReg = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fnc_contas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fnc_contas_usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdConta = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUsuario = table.Column<int>(type: "INTEGER", nullable: false),
                    Acesso = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    DthrReg = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fnc_contas_usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fnc_contas_usuarios_fnc_contas_IdConta",
                        column: x => x.IdConta,
                        principalTable: "fnc_contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fnc_contas_ContasUsuarioId",
                table: "fnc_contas",
                column: "ContasUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_fnc_contas_usuarios_IdConta",
                table: "fnc_contas_usuarios",
                column: "IdConta");

            migrationBuilder.AddForeignKey(
                name: "FK_fnc_contas_fnc_contas_usuarios_ContasUsuarioId",
                table: "fnc_contas",
                column: "ContasUsuarioId",
                principalTable: "fnc_contas_usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fnc_contas_fnc_contas_usuarios_ContasUsuarioId",
                table: "fnc_contas");

            migrationBuilder.DropTable(
                name: "fnc_contas_usuarios");

            migrationBuilder.DropTable(
                name: "fnc_contas");
        }
    }
}
