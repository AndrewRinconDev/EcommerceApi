using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApi.Migrations
{
    public partial class PaymentAndFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_Products_productId",
                table: "FavoriteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderRecords_OrderStates_orderStateId",
                table: "OrderRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "OrderStates");

            migrationBuilder.DropIndex(
                name: "IX_OrderRecords_orderStateId",
                table: "OrderRecords");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "orderStateId",
                table: "OrderRecords");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "FavoriteProducts");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "Products",
                newName: "principalProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_categoryId",
                table: "Products",
                newName: "IX_Products_principalProductId");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "FavoriteProducts",
                newName: "principalProductId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteProducts_productId",
                table: "FavoriteProducts",
                newName: "IX_FavoriteProducts_principalProductId");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "detail",
                table: "OrderRecords",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "orderState",
                table: "OrderRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "phoneNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "FeatureCategory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureCategory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ammount = table.Column<double>(type: "float", nullable: false),
                    method = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    orderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_Payment_Customers_customerId",
                        column: x => x.customerId,
                        principalTable: "Customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_Orders_orderId",
                        column: x => x.orderId,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCard",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ownerName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    company = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    expirationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cvv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    endWith = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCard", x => x.id);
                    table.ForeignKey(
                        name: "FK_PaymentCard_Customers_customerid",
                        column: x => x.customerid,
                        principalTable: "Customers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "PrincipalProducts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    categoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipalProducts", x => x.id);
                    table.ForeignKey(
                        name: "FK_PrincipalProducts_Categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    featureCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.id);
                    table.ForeignKey(
                        name: "FK_Feature_FeatureCategory_featureCategoryId",
                        column: x => x.featureCategoryId,
                        principalTable: "FeatureCategory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureProduct",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    productId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    featureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_FeatureProduct_Feature_featureId",
                        column: x => x.featureId,
                        principalTable: "Feature",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureProduct_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feature_featureCategoryId",
                table: "Feature",
                column: "featureCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureProduct_featureId",
                table: "FeatureProduct",
                column: "featureId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureProduct_productId",
                table: "FeatureProduct",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_customerId",
                table: "Payment",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_orderId",
                table: "Payment",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCard_customerid",
                table: "PaymentCard",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalProducts_categoryId",
                table: "PrincipalProducts",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_PrincipalProducts_principalProductId",
                table: "FavoriteProducts",
                column: "principalProductId",
                principalTable: "PrincipalProducts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PrincipalProducts_principalProductId",
                table: "Products",
                column: "principalProductId",
                principalTable: "PrincipalProducts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_PrincipalProducts_principalProductId",
                table: "FavoriteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_PrincipalProducts_principalProductId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "FeatureProduct");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "PaymentCard");

            migrationBuilder.DropTable(
                name: "PrincipalProducts");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "FeatureCategory");

            migrationBuilder.DropColumn(
                name: "orderState",
                table: "OrderRecords");

            migrationBuilder.RenameColumn(
                name: "principalProductId",
                table: "Products",
                newName: "categoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_principalProductId",
                table: "Products",
                newName: "IX_Products_categoryId");

            migrationBuilder.RenameColumn(
                name: "principalProductId",
                table: "FavoriteProducts",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteProducts_principalProductId",
                table: "FavoriteProducts",
                newName: "IX_FavoriteProducts_productId");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Roles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Products",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "detail",
                table: "OrderRecords",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "orderStateId",
                table: "OrderRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "FavoriteProducts",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "phoneNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OrderStates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStates", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderRecords_orderStateId",
                table: "OrderRecords",
                column: "orderStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_Products_productId",
                table: "FavoriteProducts",
                column: "productId",
                principalTable: "Products",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRecords_OrderStates_orderStateId",
                table: "OrderRecords",
                column: "orderStateId",
                principalTable: "OrderStates",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categoryId",
                table: "Products",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "id");
        }
    }
}
