using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinTrack.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddSecurities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Securities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISIN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NativeCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Securities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecurityTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderType = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Commission = table.Column<float>(type: "real", nullable: false),
                    ExchangeRate = table.Column<float>(type: "real", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityTransactions_Securities_SecurityId",
                        column: x => x.SecurityId,
                        principalTable: "Securities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Securities_ISIN",
                table: "Securities",
                column: "ISIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTransactions_SecurityId",
                table: "SecurityTransactions",
                column: "SecurityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecurityTransactions");

            migrationBuilder.DropTable(
                name: "Securities");
        }
    }
}
