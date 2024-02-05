using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalPriceColumnForProductInBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_details_product_units_product_unit_id",
                table: "order_details");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_products_product_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_users_customer_id",
                table: "orders");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "b4d74fdb-3cc7-4807-bbb9-8133b065bf6f");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "e0930f96-3729-422d-b241-d522f3ad2f83");

            migrationBuilder.AlterColumn<string>(
                name: "product_id",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "customer_id",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "product_unit_id",
                table: "order_details",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "total_price",
                table: "marked_products",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "88e6c0cc-d3ee-4ac4-8893-5c8c50158e4b", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "c7c63df4-f969-40ac-b923-dcebd77b3c1f", "88e6c0cc-d3ee-4ac4-8893-5c8c50158e4b", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });

            migrationBuilder.AddForeignKey(
                name: "fk_order_details_product_units_product_unit_id",
                table: "order_details",
                column: "product_unit_id",
                principalTable: "product_units",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_products_product_id",
                table: "orders",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_users_customer_id",
                table: "orders",
                column: "customer_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_details_product_units_product_unit_id",
                table: "order_details");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_products_product_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_users_customer_id",
                table: "orders");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "c7c63df4-f969-40ac-b923-dcebd77b3c1f");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "88e6c0cc-d3ee-4ac4-8893-5c8c50158e4b");

            migrationBuilder.DropColumn(
                name: "total_price",
                table: "marked_products");

            migrationBuilder.AlterColumn<string>(
                name: "product_id",
                table: "orders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "customer_id",
                table: "orders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "product_unit_id",
                table: "order_details",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "e0930f96-3729-422d-b241-d522f3ad2f83", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "b4d74fdb-3cc7-4807-bbb9-8133b065bf6f", "e0930f96-3729-422d-b241-d522f3ad2f83", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });

            migrationBuilder.AddForeignKey(
                name: "fk_order_details_product_units_product_unit_id",
                table: "order_details",
                column: "product_unit_id",
                principalTable: "product_units",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_products_product_id",
                table: "orders",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_users_customer_id",
                table: "orders",
                column: "customer_id",
                principalTable: "AspNetUsers",
                principalColumn: "id");
        }
    }
}
