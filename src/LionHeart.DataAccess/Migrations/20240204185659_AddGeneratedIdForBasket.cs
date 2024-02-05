using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGeneratedIdForBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "1f8d43b1-0ace-40b2-af2e-f1b9f99c90be");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "d262e3d3-5966-4083-a3c1-63e1f942dbb4");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "baskets",
                type: "text",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "d4d6034a-3e53-48b8-92a5-2f0dc819de74", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "f3f57380-12a0-40cd-a9ae-dd9828d7a8be", "d4d6034a-3e53-48b8-92a5-2f0dc819de74", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "f3f57380-12a0-40cd-a9ae-dd9828d7a8be");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "d4d6034a-3e53-48b8-92a5-2f0dc819de74");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "baskets",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "d262e3d3-5966-4083-a3c1-63e1f942dbb4", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "1f8d43b1-0ace-40b2-af2e-f1b9f99c90be", "d262e3d3-5966-4083-a3c1-63e1f942dbb4", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });
        }
    }
}
