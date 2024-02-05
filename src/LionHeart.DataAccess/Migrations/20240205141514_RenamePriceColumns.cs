using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenamePriceColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "208fb601-7ef1-4c8b-a9ec-a5280942f266");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "4c963bb0-7454-4970-bad1-36679130340f");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "77b4697d-6e7e-4a56-96ea-009c29d9b9ee");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "e3888866-c004-4ba7-9acc-9fa9eda8ece0");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "ef9b4a62-54da-4451-8eb1-5648912721ec");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "309c1b91-c0c9-488c-b79c-0f562855fc2e");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "cb32be6e-ad47-48ed-a7ca-f9e350c7cc47");

            migrationBuilder.RenameColumn(
                name: "total_price",
                table: "marked_products",
                newName: "products_total_price");

            migrationBuilder.RenameColumn(
                name: "total_price",
                table: "baskets",
                newName: "basket_total_price");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "de26bf83-a5c3-42e2-b923-577020498400", "Одежда" });

            migrationBuilder.InsertData(
                table: "product_units",
                columns: new[] { "id", "created_at", "product_id", "sale_status", "sold_on" },
                values: new object[,]
                {
                    { "0ebcd6de-f010-44f3-964d-7b77edb9caca", new DateTimeOffset(new DateTime(2024, 2, 5, 17, 15, 13, 821, DateTimeKind.Unspecified).AddTicks(955), new TimeSpan(0, 3, 0, 0, 0)), "282fee71-b446-4b67-98f6-54eb760dd913", 0, null },
                    { "63509bdd-92bb-4dec-a124-9176f16f179e", new DateTimeOffset(new DateTime(2024, 2, 5, 17, 15, 13, 821, DateTimeKind.Unspecified).AddTicks(845), new TimeSpan(0, 3, 0, 0, 0)), "282fee71-b446-4b67-98f6-54eb760dd913", 0, null },
                    { "7a0ce113-8100-4e80-a283-a9d50fdf628c", new DateTimeOffset(new DateTime(2024, 2, 5, 17, 15, 13, 821, DateTimeKind.Unspecified).AddTicks(934), new TimeSpan(0, 3, 0, 0, 0)), "282fee71-b446-4b67-98f6-54eb760dd913", 0, null },
                    { "aa43bcd1-8b99-4f69-bc94-5626e2752a98", new DateTimeOffset(new DateTime(2024, 2, 5, 17, 15, 13, 821, DateTimeKind.Unspecified).AddTicks(757), new TimeSpan(0, 3, 0, 0, 0)), "282fee71-b446-4b67-98f6-54eb760dd913", 0, null },
                    { "d8d131bc-47d5-4bb0-83f0-13c45f0b799c", new DateTimeOffset(new DateTime(2024, 2, 5, 17, 15, 13, 821, DateTimeKind.Unspecified).AddTicks(823), new TimeSpan(0, 3, 0, 0, 0)), "282fee71-b446-4b67-98f6-54eb760dd913", 0, null }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "282fee71-b446-4b67-98f6-54eb760dd913", "de26bf83-a5c3-42e2-b923-577020498400", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "0ebcd6de-f010-44f3-964d-7b77edb9caca");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "63509bdd-92bb-4dec-a124-9176f16f179e");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "7a0ce113-8100-4e80-a283-a9d50fdf628c");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "aa43bcd1-8b99-4f69-bc94-5626e2752a98");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "d8d131bc-47d5-4bb0-83f0-13c45f0b799c");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "282fee71-b446-4b67-98f6-54eb760dd913");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "de26bf83-a5c3-42e2-b923-577020498400");

            migrationBuilder.RenameColumn(
                name: "products_total_price",
                table: "marked_products",
                newName: "total_price");

            migrationBuilder.RenameColumn(
                name: "basket_total_price",
                table: "baskets",
                newName: "total_price");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "cb32be6e-ad47-48ed-a7ca-f9e350c7cc47", "Одежда" });

            migrationBuilder.InsertData(
                table: "product_units",
                columns: new[] { "id", "created_at", "product_id", "sale_status", "sold_on" },
                values: new object[,]
                {
                    { "208fb601-7ef1-4c8b-a9ec-a5280942f266", new DateTimeOffset(new DateTime(2024, 2, 5, 14, 6, 16, 554, DateTimeKind.Unspecified).AddTicks(1874), new TimeSpan(0, 3, 0, 0, 0)), "309c1b91-c0c9-488c-b79c-0f562855fc2e", 0, null },
                    { "4c963bb0-7454-4970-bad1-36679130340f", new DateTimeOffset(new DateTime(2024, 2, 5, 14, 6, 16, 554, DateTimeKind.Unspecified).AddTicks(1794), new TimeSpan(0, 3, 0, 0, 0)), "309c1b91-c0c9-488c-b79c-0f562855fc2e", 0, null },
                    { "77b4697d-6e7e-4a56-96ea-009c29d9b9ee", new DateTimeOffset(new DateTime(2024, 2, 5, 14, 6, 16, 554, DateTimeKind.Unspecified).AddTicks(1929), new TimeSpan(0, 3, 0, 0, 0)), "309c1b91-c0c9-488c-b79c-0f562855fc2e", 0, null },
                    { "e3888866-c004-4ba7-9acc-9fa9eda8ece0", new DateTimeOffset(new DateTime(2024, 2, 5, 14, 6, 16, 554, DateTimeKind.Unspecified).AddTicks(1857), new TimeSpan(0, 3, 0, 0, 0)), "309c1b91-c0c9-488c-b79c-0f562855fc2e", 0, null },
                    { "ef9b4a62-54da-4451-8eb1-5648912721ec", new DateTimeOffset(new DateTime(2024, 2, 5, 14, 6, 16, 554, DateTimeKind.Unspecified).AddTicks(1890), new TimeSpan(0, 3, 0, 0, 0)), "309c1b91-c0c9-488c-b79c-0f562855fc2e", 0, null }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "309c1b91-c0c9-488c-b79c-0f562855fc2e", "cb32be6e-ad47-48ed-a7ca-f9e350c7cc47", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });
        }
    }
}
