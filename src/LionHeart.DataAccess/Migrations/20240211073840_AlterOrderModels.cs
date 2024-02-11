using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AlterOrderModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_products_product_id",
                table: "orders");

            migrationBuilder.DropTable(
                name: "order_details");

            migrationBuilder.DropIndex(
                name: "ix_orders_product_id",
                table: "orders");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "3b4c0d14-84e9-4eb8-8084-88a53e404b6f");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "5821f1ac-5416-417a-b62f-58d68e4e7e90");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "5eab2f2e-e9c9-43be-be38-50bcb79bf567");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "b8256397-e98f-4f34-b793-be94fe84bebd");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "d60cc6f0-36fb-4bd9-8b7e-930b31054677");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "ca971fc7-c5b4-4265-a7e6-c54b807d85c5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "a2e958de-d704-477c-8fee-7b521494e18f");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "0b9ffdbc-3ab2-4816-a434-3a07c989577e");

            migrationBuilder.DropColumn(
                name: "product_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "orders");

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    order_id = table.Column<string>(type: "text", nullable: false),
                    product_id = table.Column<string>(type: "text", nullable: false),
                    product_price = table.Column<decimal>(type: "numeric", nullable: false),
                    product_quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_item_details",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    order_item_id = table.Column<string>(type: "text", nullable: false),
                    product_unit_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_item_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_item_details_order_items_order_item_id",
                        column: x => x.order_item_id,
                        principalTable: "order_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "personal_discount", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "6511dfde-30e7-4581-94c8-6ec7eb2516c1", 0, "d65b4895-2028-4040-bf94-3dcadddbe437", "admin", false, null, null, false, null, null, null, null, 0m, null, false, "926e12c7-f7d2-47b7-9b64-92a184fb2975", false, "admin" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "9452c798-a228-4b36-92f0-acfc838def3c", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "specifications", "user_id" },
                values: new object[] { "f81cee91-b971-474a-a7a7-c7e2fd3e90f5", "9452c798-a228-4b36-92f0-acfc838def3c", "Красивая и удобная футболка", "Футболка", 1250m, "Размер - XXL", "6511dfde-30e7-4581-94c8-6ec7eb2516c1" });

            migrationBuilder.InsertData(
                table: "product_units",
                columns: new[] { "id", "created_at", "product_id", "sale_status" },
                values: new object[,]
                {
                    { "1634e4cf-ce4b-4912-945f-f712a38ce03c", new DateTimeOffset(new DateTime(2024, 2, 11, 10, 38, 39, 508, DateTimeKind.Unspecified).AddTicks(565), new TimeSpan(0, 3, 0, 0, 0)), "f81cee91-b971-474a-a7a7-c7e2fd3e90f5", 0 },
                    { "2eab019d-4cc4-4145-81ba-63f455397e6a", new DateTimeOffset(new DateTime(2024, 2, 11, 10, 38, 39, 508, DateTimeKind.Unspecified).AddTicks(501), new TimeSpan(0, 3, 0, 0, 0)), "f81cee91-b971-474a-a7a7-c7e2fd3e90f5", 0 },
                    { "71cf0489-534b-431c-8b9b-7571834ad8f0", new DateTimeOffset(new DateTime(2024, 2, 11, 10, 38, 39, 508, DateTimeKind.Unspecified).AddTicks(602), new TimeSpan(0, 3, 0, 0, 0)), "f81cee91-b971-474a-a7a7-c7e2fd3e90f5", 0 },
                    { "ae455eb5-18ef-4cd8-a00e-ec7c30f5427b", new DateTimeOffset(new DateTime(2024, 2, 11, 10, 38, 39, 508, DateTimeKind.Unspecified).AddTicks(619), new TimeSpan(0, 3, 0, 0, 0)), "f81cee91-b971-474a-a7a7-c7e2fd3e90f5", 0 },
                    { "beada3f1-612e-4ffc-91c4-b60640ebfa7a", new DateTimeOffset(new DateTime(2024, 2, 11, 10, 38, 39, 508, DateTimeKind.Unspecified).AddTicks(584), new TimeSpan(0, 3, 0, 0, 0)), "f81cee91-b971-474a-a7a7-c7e2fd3e90f5", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_item_details_order_item_id",
                table: "order_item_details",
                column: "order_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "order_items",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_item_details");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "1634e4cf-ce4b-4912-945f-f712a38ce03c");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "2eab019d-4cc4-4145-81ba-63f455397e6a");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "71cf0489-534b-431c-8b9b-7571834ad8f0");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "ae455eb5-18ef-4cd8-a00e-ec7c30f5427b");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "beada3f1-612e-4ffc-91c4-b60640ebfa7a");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "f81cee91-b971-474a-a7a7-c7e2fd3e90f5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "6511dfde-30e7-4581-94c8-6ec7eb2516c1");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "9452c798-a228-4b36-92f0-acfc838def3c");

            migrationBuilder.AddColumn<string>(
                name: "product_id",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "order_details",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    product_unit_id = table.Column<string>(type: "text", nullable: false),
                    order_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_details_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_details_product_units_product_unit_id",
                        column: x => x.product_unit_id,
                        principalTable: "product_units",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "personal_discount", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "a2e958de-d704-477c-8fee-7b521494e18f", 0, "9fdf0f50-de99-4a3f-b547-05abaa4e26b5", "admin", false, null, null, false, null, null, null, null, 0m, null, false, "b0bd650c-fde9-4d84-b91d-49123e882502", false, "admin" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "0b9ffdbc-3ab2-4816-a434-3a07c989577e", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "specifications", "user_id" },
                values: new object[] { "ca971fc7-c5b4-4265-a7e6-c54b807d85c5", "0b9ffdbc-3ab2-4816-a434-3a07c989577e", "Красивая и удобная футболка", "Футболка", 1250m, "Размер - XXL", "a2e958de-d704-477c-8fee-7b521494e18f" });

            migrationBuilder.InsertData(
                table: "product_units",
                columns: new[] { "id", "created_at", "product_id", "sale_status" },
                values: new object[,]
                {
                    { "3b4c0d14-84e9-4eb8-8084-88a53e404b6f", new DateTimeOffset(new DateTime(2024, 2, 10, 9, 54, 39, 212, DateTimeKind.Unspecified).AddTicks(3550), new TimeSpan(0, 3, 0, 0, 0)), "ca971fc7-c5b4-4265-a7e6-c54b807d85c5", 0 },
                    { "5821f1ac-5416-417a-b62f-58d68e4e7e90", new DateTimeOffset(new DateTime(2024, 2, 10, 9, 54, 39, 212, DateTimeKind.Unspecified).AddTicks(3611), new TimeSpan(0, 3, 0, 0, 0)), "ca971fc7-c5b4-4265-a7e6-c54b807d85c5", 0 },
                    { "5eab2f2e-e9c9-43be-be38-50bcb79bf567", new DateTimeOffset(new DateTime(2024, 2, 10, 9, 54, 39, 212, DateTimeKind.Unspecified).AddTicks(3649), new TimeSpan(0, 3, 0, 0, 0)), "ca971fc7-c5b4-4265-a7e6-c54b807d85c5", 0 },
                    { "b8256397-e98f-4f34-b793-be94fe84bebd", new DateTimeOffset(new DateTime(2024, 2, 10, 9, 54, 39, 212, DateTimeKind.Unspecified).AddTicks(3668), new TimeSpan(0, 3, 0, 0, 0)), "ca971fc7-c5b4-4265-a7e6-c54b807d85c5", 0 },
                    { "d60cc6f0-36fb-4bd9-8b7e-930b31054677", new DateTimeOffset(new DateTime(2024, 2, 10, 9, 54, 39, 212, DateTimeKind.Unspecified).AddTicks(3630), new TimeSpan(0, 3, 0, 0, 0)), "ca971fc7-c5b4-4265-a7e6-c54b807d85c5", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_orders_product_id",
                table: "orders",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_details_order_id",
                table: "order_details",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_details_product_unit_id",
                table: "order_details",
                column: "product_unit_id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_products_product_id",
                table: "orders",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
