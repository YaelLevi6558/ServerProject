using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__19093A0BB84D2D89", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    DonorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Donors__052E3F788362A914", x => x.DonorId);
                });

            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    GiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiftName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    DonorId = table.Column<int>(type: "int", nullable: false),
                    TicketCost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Gifts__4A40E60508CE312E", x => x.GiftId);
                    table.ForeignKey(
                        name: "FK__Gifts__CategoryI__412EB0B6",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK__Gifts__DonorId__4222D4EF",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "DonorId");
                });

            migrationBuilder.CreateTable(
                name: "Optional",
                columns: table => new
                {
                    WinnerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiftId = table.Column<int>(type: "int", nullable: false),
                    WinnerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WinnerEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WinnerPhone = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    WinningDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Optional__8A3D1DA87AC29E96", x => x.WinnerId);
                    table.ForeignKey(
                        name: "FK__Optional__GiftId__48CFD27E",
                        column: x => x.GiftId,
                        principalTable: "Gifts",
                        principalColumn: "GiftId");
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiftId = table.Column<int>(type: "int", nullable: false),
                    BuyerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BuyerEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumberOfTickets = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    BuyerPhone = table.Column<decimal>(type: "decimal(10,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Purchase__6B0A6BBE95245DBB", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK__Purchases__GiftI__44FF419A",
                        column: x => x.GiftId,
                        principalTable: "Gifts",
                        principalColumn: "GiftId");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Donors__A9D105346A61C5DE",
                table: "Donors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_CategoryId",
                table: "Gifts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_DonorId",
                table: "Gifts",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_Optional_GiftId",
                table: "Optional",
                column: "GiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_GiftId",
                table: "Purchases",
                column: "GiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Optional");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Gifts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Donors");
        }
    }
}
