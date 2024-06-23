using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApi.Migrations
{
    public partial class UpdateOrderHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRecords");

            migrationBuilder.CreateTable(
                name: "OrderHistories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    orderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    orderState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistories", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderHistories_Orders_orderId",
                        column: x => x.orderId,
                        principalTable: "Orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_orderId",
                table: "OrderHistories",
                column: "orderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderHistories");

            migrationBuilder.CreateTable(
                name: "OrderRecords",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    orderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orderState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRecords", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderRecords_Orders_orderId",
                        column: x => x.orderId,
                        principalTable: "Orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderRecords_orderId",
                table: "OrderRecords",
                column: "orderId");
        }
    }
}
