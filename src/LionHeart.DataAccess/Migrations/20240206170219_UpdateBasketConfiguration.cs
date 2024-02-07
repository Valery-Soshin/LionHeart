using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBasketConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_marked_products_baskets_basket_id",
                table: "marked_products");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "089cb3c5-4dc7-4f37-bacd-c789f1831faa");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "66aaaaf3-020f-48de-867a-8f6732b01351");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "6ba575c3-2b36-4625-904e-b59aa932662e");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "873c0ea6-75af-4c6c-86ba-7d1ca2cd16ed");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "baf5f4df-2a71-4d06-bb40-33df653567cc");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "7e966665-7d17-4886-9f9e-909c5936b5eb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "b198a07a-19e1-4629-9ef0-c52d3c093d17");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "324fb13d-d13b-4c32-9ef4-1495a0f1acb2");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "personal_discount", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "b8376d9e-a408-4e50-8918-065b611daae3", 0, "33fd3150-5b65-441d-986a-51a4ccdc3028", "admin", false, null, null, false, null, null, null, null, 0m, null, false, "1ebdcfb5-24c9-4121-923c-b6ab876f062e", false, "admin" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "e40c6319-5296-422c-8703-d72f43d18c65", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "user_id" },
                values: new object[] { "cff3175a-5a00-43d6-afc1-0291c43cb2ab", "e40c6319-5296-422c-8703-d72f43d18c65", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", "b8376d9e-a408-4e50-8918-065b611daae3" });

            migrationBuilder.InsertData(
                table: "product_units",
                columns: new[] { "id", "created_at", "product_id", "sale_status" },
                values: new object[,]
                {
                    { "07533bc1-bae5-448a-bdce-14eb21b2f8f5", new DateTimeOffset(new DateTime(2024, 2, 6, 20, 2, 18, 616, DateTimeKind.Unspecified).AddTicks(4200), new TimeSpan(0, 3, 0, 0, 0)), "cff3175a-5a00-43d6-afc1-0291c43cb2ab", 0 },
                    { "0cdec91a-b280-4b51-9549-89e6a3386bfd", new DateTimeOffset(new DateTime(2024, 2, 6, 20, 2, 18, 616, DateTimeKind.Unspecified).AddTicks(4279), new TimeSpan(0, 3, 0, 0, 0)), "cff3175a-5a00-43d6-afc1-0291c43cb2ab", 0 },
                    { "3bbd5637-fb5f-4bc6-ba47-c3e6cff0aa28", new DateTimeOffset(new DateTime(2024, 2, 6, 20, 2, 18, 616, DateTimeKind.Unspecified).AddTicks(4261), new TimeSpan(0, 3, 0, 0, 0)), "cff3175a-5a00-43d6-afc1-0291c43cb2ab", 0 },
                    { "6773defa-ed10-4588-b920-cde4bf2ce6df", new DateTimeOffset(new DateTime(2024, 2, 6, 20, 2, 18, 616, DateTimeKind.Unspecified).AddTicks(4134), new TimeSpan(0, 3, 0, 0, 0)), "cff3175a-5a00-43d6-afc1-0291c43cb2ab", 0 },
                    { "e7824b5b-7460-4f0e-b02b-3bb8a1582791", new DateTimeOffset(new DateTime(2024, 2, 6, 20, 2, 18, 616, DateTimeKind.Unspecified).AddTicks(4243), new TimeSpan(0, 3, 0, 0, 0)), "cff3175a-5a00-43d6-afc1-0291c43cb2ab", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_marked_products_baskets_basket_id",
                table: "marked_products",
                column: "basket_id",
                principalTable: "baskets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_marked_products_baskets_basket_id",
                table: "marked_products");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "07533bc1-bae5-448a-bdce-14eb21b2f8f5");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "0cdec91a-b280-4b51-9549-89e6a3386bfd");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "3bbd5637-fb5f-4bc6-ba47-c3e6cff0aa28");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "6773defa-ed10-4588-b920-cde4bf2ce6df");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "e7824b5b-7460-4f0e-b02b-3bb8a1582791");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "cff3175a-5a00-43d6-afc1-0291c43cb2ab");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "b8376d9e-a408-4e50-8918-065b611daae3");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "e40c6319-5296-422c-8703-d72f43d18c65");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "personal_discount", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "b198a07a-19e1-4629-9ef0-c52d3c093d17", 0, "7cec0bb5-3807-4b63-877e-535513115ec3", "admin", false, null, null, false, null, null, null, null, 0m, null, false, "e59a019b-4b0f-4065-b452-d22c0f658668", false, "admin" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "324fb13d-d13b-4c32-9ef4-1495a0f1acb2", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "user_id" },
                values: new object[] { "7e966665-7d17-4886-9f9e-909c5936b5eb", "324fb13d-d13b-4c32-9ef4-1495a0f1acb2", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", "b198a07a-19e1-4629-9ef0-c52d3c093d17" });

            migrationBuilder.InsertData(
                table: "product_units",
                columns: new[] { "id", "created_at", "product_id", "sale_status" },
                values: new object[,]
                {
                    { "089cb3c5-4dc7-4f37-bacd-c789f1831faa", new DateTimeOffset(new DateTime(2024, 2, 6, 18, 47, 56, 673, DateTimeKind.Unspecified).AddTicks(2636), new TimeSpan(0, 3, 0, 0, 0)), "7e966665-7d17-4886-9f9e-909c5936b5eb", 0 },
                    { "66aaaaf3-020f-48de-867a-8f6732b01351", new DateTimeOffset(new DateTime(2024, 2, 6, 18, 47, 56, 673, DateTimeKind.Unspecified).AddTicks(2734), new TimeSpan(0, 3, 0, 0, 0)), "7e966665-7d17-4886-9f9e-909c5936b5eb", 0 },
                    { "6ba575c3-2b36-4625-904e-b59aa932662e", new DateTimeOffset(new DateTime(2024, 2, 6, 18, 47, 56, 673, DateTimeKind.Unspecified).AddTicks(2697), new TimeSpan(0, 3, 0, 0, 0)), "7e966665-7d17-4886-9f9e-909c5936b5eb", 0 },
                    { "873c0ea6-75af-4c6c-86ba-7d1ca2cd16ed", new DateTimeOffset(new DateTime(2024, 2, 6, 18, 47, 56, 673, DateTimeKind.Unspecified).AddTicks(2752), new TimeSpan(0, 3, 0, 0, 0)), "7e966665-7d17-4886-9f9e-909c5936b5eb", 0 },
                    { "baf5f4df-2a71-4d06-bb40-33df653567cc", new DateTimeOffset(new DateTime(2024, 2, 6, 18, 47, 56, 673, DateTimeKind.Unspecified).AddTicks(2714), new TimeSpan(0, 3, 0, 0, 0)), "7e966665-7d17-4886-9f9e-909c5936b5eb", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_marked_products_baskets_basket_id",
                table: "marked_products",
                column: "basket_id",
                principalTable: "baskets",
                principalColumn: "id");
        }
    }
}
