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
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isin = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    native_curency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Securities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "security_transactions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_type = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    comission = table.Column<float>(type: "real", nullable: false),
                    exchange_rate = table.Column<float>(type: "real", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    security_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_security_transactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_security_transactions_Securities_security_id",
                        column: x => x.security_id,
                        principalTable: "Securities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Securities_isin",
                table: "Securities",
                column: "isin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_security_transactions_security_id",
                table: "security_transactions",
                column: "security_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "security_transactions");

            migrationBuilder.DropTable(
                name: "Securities");
        }
    }
}
