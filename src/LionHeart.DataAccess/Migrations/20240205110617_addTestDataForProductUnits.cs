using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTestDataForProductUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "c7c63df4-f969-40ac-b923-dcebd77b3c1f");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "88e6c0cc-d3ee-4ac4-8893-5c8c50158e4b");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "88e6c0cc-d3ee-4ac4-8893-5c8c50158e4b", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "c7c63df4-f969-40ac-b923-dcebd77b3c1f", "88e6c0cc-d3ee-4ac4-8893-5c8c50158e4b", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });
        }
    }
}
