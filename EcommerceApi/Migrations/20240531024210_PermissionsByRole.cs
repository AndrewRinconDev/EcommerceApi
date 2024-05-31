using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApi.Migrations
{
    public partial class PermissionsByRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feature_FeatureCategory_featureCategoryId",
                table: "Feature");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureProduct_Feature_featureId",
                table: "FeatureProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureProduct_Products_productId",
                table: "FeatureProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Customers_customerId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Orders_orderId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCard_Customers_customerid",
                table: "PaymentCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentCard",
                table: "PaymentCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeatureProduct",
                table: "FeatureProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeatureCategory",
                table: "FeatureCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feature",
                table: "Feature");

            migrationBuilder.RenameTable(
                name: "PaymentCard",
                newName: "PaymentCards");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "FeatureProduct",
                newName: "FeatureProducts");

            migrationBuilder.RenameTable(
                name: "FeatureCategory",
                newName: "FeatureCategories");

            migrationBuilder.RenameTable(
                name: "Feature",
                newName: "Features");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentCard_customerid",
                table: "PaymentCards",
                newName: "IX_PaymentCards_customerid");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_orderId",
                table: "Payments",
                newName: "IX_Payments_orderId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_customerId",
                table: "Payments",
                newName: "IX_Payments_customerId");

            migrationBuilder.RenameIndex(
                name: "IX_FeatureProduct_productId",
                table: "FeatureProducts",
                newName: "IX_FeatureProducts_productId");

            migrationBuilder.RenameIndex(
                name: "IX_FeatureProduct_featureId",
                table: "FeatureProducts",
                newName: "IX_FeatureProducts_featureId");

            migrationBuilder.RenameIndex(
                name: "IX_Feature_featureCategoryId",
                table: "Features",
                newName: "IX_Features_featureCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentCards",
                table: "PaymentCards",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeatureProducts",
                table: "FeatureProducts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeatureCategories",
                table: "FeatureCategories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Features",
                table: "Features",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    permissionType = table.Column<int>(type: "int", nullable: true),
                    roleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_roleId",
                table: "Permissions",
                column: "roleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureProducts_Features_featureId",
                table: "FeatureProducts",
                column: "featureId",
                principalTable: "Features",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureProducts_Products_productId",
                table: "FeatureProducts",
                column: "productId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Features_FeatureCategories_featureCategoryId",
                table: "Features",
                column: "featureCategoryId",
                principalTable: "FeatureCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCards_Customers_customerid",
                table: "PaymentCards",
                column: "customerid",
                principalTable: "Customers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Customers_customerId",
                table: "Payments",
                column: "customerId",
                principalTable: "Customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_orderId",
                table: "Payments",
                column: "orderId",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureProducts_Features_featureId",
                table: "FeatureProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureProducts_Products_productId",
                table: "FeatureProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_FeatureCategories_featureCategoryId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCards_Customers_customerid",
                table: "PaymentCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Customers_customerId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_orderId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentCards",
                table: "PaymentCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Features",
                table: "Features");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeatureProducts",
                table: "FeatureProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeatureCategories",
                table: "FeatureCategories");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameTable(
                name: "PaymentCards",
                newName: "PaymentCard");

            migrationBuilder.RenameTable(
                name: "Features",
                newName: "Feature");

            migrationBuilder.RenameTable(
                name: "FeatureProducts",
                newName: "FeatureProduct");

            migrationBuilder.RenameTable(
                name: "FeatureCategories",
                newName: "FeatureCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_orderId",
                table: "Payment",
                newName: "IX_Payment_orderId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_customerId",
                table: "Payment",
                newName: "IX_Payment_customerId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentCards_customerid",
                table: "PaymentCard",
                newName: "IX_PaymentCard_customerid");

            migrationBuilder.RenameIndex(
                name: "IX_Features_featureCategoryId",
                table: "Feature",
                newName: "IX_Feature_featureCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_FeatureProducts_productId",
                table: "FeatureProduct",
                newName: "IX_FeatureProduct_productId");

            migrationBuilder.RenameIndex(
                name: "IX_FeatureProducts_featureId",
                table: "FeatureProduct",
                newName: "IX_FeatureProduct_featureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentCard",
                table: "PaymentCard",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feature",
                table: "Feature",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeatureProduct",
                table: "FeatureProduct",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeatureCategory",
                table: "FeatureCategory",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feature_FeatureCategory_featureCategoryId",
                table: "Feature",
                column: "featureCategoryId",
                principalTable: "FeatureCategory",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureProduct_Feature_featureId",
                table: "FeatureProduct",
                column: "featureId",
                principalTable: "Feature",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureProduct_Products_productId",
                table: "FeatureProduct",
                column: "productId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Customers_customerId",
                table: "Payment",
                column: "customerId",
                principalTable: "Customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Orders_orderId",
                table: "Payment",
                column: "orderId",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCard_Customers_customerid",
                table: "PaymentCard",
                column: "customerid",
                principalTable: "Customers",
                principalColumn: "id");
        }
    }
}
