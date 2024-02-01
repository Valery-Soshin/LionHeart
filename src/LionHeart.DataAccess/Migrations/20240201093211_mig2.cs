using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "c29f15ea-6738-4cd8-a1c1-03bfffba9aaf");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "d2024647-69c9-4594-a765-2cd3ac55d026");

            migrationBuilder.AddUniqueConstraint(
                name: "ak_marked_products_customer_id_product_id",
                table: "marked_products",
                columns: new[] { "customer_id", "product_id" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "05ecbe31-525b-43a6-ab91-beb844b8db3c", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "0b5920c7-03e8-4d89-93d4-fc42eadd5d66", "05ecbe31-525b-43a6-ab91-beb844b8db3c", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "ak_marked_products_customer_id_product_id",
                table: "marked_products");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "0b5920c7-03e8-4d89-93d4-fc42eadd5d66");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "05ecbe31-525b-43a6-ab91-beb844b8db3c");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "d2024647-69c9-4594-a765-2cd3ac55d026", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "c29f15ea-6738-4cd8-a1c1-03bfffba9aaf", "d2024647-69c9-4594-a765-2cd3ac55d026", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });
        }
    }
}
