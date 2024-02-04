using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_products_product_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "fk_order_users_customer_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "fk_order_details_product_details_product_detail_id",
                table: "order_details");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order",
                table: "order");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "0b5920c7-03e8-4d89-93d4-fc42eadd5d66");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "05ecbe31-525b-43a6-ab91-beb844b8db3c");

            migrationBuilder.RenameTable(
                name: "order",
                newName: "orders");

            migrationBuilder.RenameIndex(
                name: "ix_order_product_id",
                table: "orders",
                newName: "ix_orders_product_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_customer_id",
                table: "orders",
                newName: "ix_orders_customer_id");

            migrationBuilder.AlterColumn<string>(
                name: "product_detail_id",
                table: "order_details",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_orders",
                table: "orders",
                column: "id");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "cd3f58ce-53c6-4400-9514-428f2285f8ec", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "d9946d62-f35e-46cd-ae23-2ab4eb723e36", "cd3f58ce-53c6-4400-9514-428f2285f8ec", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });

            migrationBuilder.AddForeignKey(
                name: "fk_order_details_product_details_product_detail_id",
                table: "order_details",
                column: "product_detail_id",
                principalTable: "product_details",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_details_product_details_product_detail_id",
                table: "order_details");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_products_product_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_users_customer_id",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "pk_orders",
                table: "orders");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "d9946d62-f35e-46cd-ae23-2ab4eb723e36");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "cd3f58ce-53c6-4400-9514-428f2285f8ec");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "order");

            migrationBuilder.RenameIndex(
                name: "ix_orders_product_id",
                table: "order",
                newName: "ix_order_product_id");

            migrationBuilder.RenameIndex(
                name: "ix_orders_customer_id",
                table: "order",
                newName: "ix_order_customer_id");

            migrationBuilder.AlterColumn<string>(
                name: "product_detail_id",
                table: "order_details",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order",
                table: "order",
                column: "id");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "05ecbe31-525b-43a6-ab91-beb844b8db3c", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "0b5920c7-03e8-4d89-93d4-fc42eadd5d66", "05ecbe31-525b-43a6-ab91-beb844b8db3c", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });

            migrationBuilder.AddForeignKey(
                name: "fk_order_products_product_id",
                table: "order",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_users_customer_id",
                table: "order",
                column: "customer_id",
                principalTable: "AspNetUsers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_details_product_details_product_detail_id",
                table: "order_details",
                column: "product_detail_id",
                principalTable: "product_details",
                principalColumn: "id");
        }
    }
}
