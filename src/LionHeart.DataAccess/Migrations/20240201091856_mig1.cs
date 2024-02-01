using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "d2024647-69c9-4594-a765-2cd3ac55d026", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "c29f15ea-6738-4cd8-a1c1-03bfffba9aaf", "d2024647-69c9-4594-a765-2cd3ac55d026", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "c29f15ea-6738-4cd8-a1c1-03bfffba9aaf");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "d2024647-69c9-4594-a765-2cd3ac55d026");
        }
    }
}
