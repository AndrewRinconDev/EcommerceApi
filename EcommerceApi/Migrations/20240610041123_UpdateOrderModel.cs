using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApi.Migrations
{
    public partial class UpdateOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateUpdate",
                table: "Orders",
                newName: "updateOn");

            migrationBuilder.RenameColumn(
                name: "creationDates",
                table: "Orders",
                newName: "creationOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updateOn",
                table: "Orders",
                newName: "dateUpdate");

            migrationBuilder.RenameColumn(
                name: "creationOn",
                table: "Orders",
                newName: "creationDates");
        }
    }
}
