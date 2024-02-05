using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalPriceColumnForBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "f3f57380-12a0-40cd-a9ae-dd9828d7a8be");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "d4d6034a-3e53-48b8-92a5-2f0dc819de74");

            migrationBuilder.AddColumn<decimal>(
                name: "total_price",
                table: "baskets",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "e0930f96-3729-422d-b241-d522f3ad2f83", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "b4d74fdb-3cc7-4807-bbb9-8133b065bf6f", "e0930f96-3729-422d-b241-d522f3ad2f83", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "b4d74fdb-3cc7-4807-bbb9-8133b065bf6f");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "e0930f96-3729-422d-b241-d522f3ad2f83");

            migrationBuilder.DropColumn(
                name: "total_price",
                table: "baskets");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "d4d6034a-3e53-48b8-92a5-2f0dc819de74", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "f3f57380-12a0-40cd-a9ae-dd9828d7a8be", "d4d6034a-3e53-48b8-92a5-2f0dc819de74", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });
        }
    }
}
