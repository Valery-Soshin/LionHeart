using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableProductUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "2dfc7803-a55d-4208-b9b2-61913a3efc5f");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "47028a9a-5bc0-4b75-b945-4c0c7686110a");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "a08498d6-f1f0-4ee9-ab99-80acae0a56c6");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "e10b401f-3f5f-4aa9-b966-2682084d9ada");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "ea10cb03-c95b-4e86-9246-4230bebfed23");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "00447021-43b4-4f6b-ae93-c61960043cd6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "a0afbf8a-373b-4750-b523-ab9e9eff1550");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "5c2daf84-a029-482e-a537-500c34855ad9");

            migrationBuilder.DropColumn(
                name: "sold_on",
                table: "product_units");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "sold_on",
                table: "product_units",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "personal_discount", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "a0afbf8a-373b-4750-b523-ab9e9eff1550", 0, "c13140ec-3104-4256-881c-125acb7880e1", "admin", false, null, null, false, null, null, null, null, 0m, null, false, "006753d0-e19d-4d33-b8b1-9beb37461923", false, "admin" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "5c2daf84-a029-482e-a537-500c34855ad9", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "user_id" },
                values: new object[] { "00447021-43b4-4f6b-ae93-c61960043cd6", "5c2daf84-a029-482e-a537-500c34855ad9", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", "a0afbf8a-373b-4750-b523-ab9e9eff1550" });

            migrationBuilder.InsertData(
                table: "product_units",
                columns: new[] { "id", "created_at", "product_id", "sale_status", "sold_on" },
                values: new object[,]
                {
                    { "2dfc7803-a55d-4208-b9b2-61913a3efc5f", new DateTimeOffset(new DateTime(2024, 2, 5, 19, 14, 51, 82, DateTimeKind.Unspecified).AddTicks(5091), new TimeSpan(0, 3, 0, 0, 0)), "00447021-43b4-4f6b-ae93-c61960043cd6", 0, null },
                    { "47028a9a-5bc0-4b75-b945-4c0c7686110a", new DateTimeOffset(new DateTime(2024, 2, 5, 19, 14, 51, 82, DateTimeKind.Unspecified).AddTicks(5143), new TimeSpan(0, 3, 0, 0, 0)), "00447021-43b4-4f6b-ae93-c61960043cd6", 0, null },
                    { "a08498d6-f1f0-4ee9-ab99-80acae0a56c6", new DateTimeOffset(new DateTime(2024, 2, 5, 19, 14, 51, 82, DateTimeKind.Unspecified).AddTicks(5119), new TimeSpan(0, 3, 0, 0, 0)), "00447021-43b4-4f6b-ae93-c61960043cd6", 0, null },
                    { "e10b401f-3f5f-4aa9-b966-2682084d9ada", new DateTimeOffset(new DateTime(2024, 2, 5, 19, 14, 51, 82, DateTimeKind.Unspecified).AddTicks(4993), new TimeSpan(0, 3, 0, 0, 0)), "00447021-43b4-4f6b-ae93-c61960043cd6", 0, null },
                    { "ea10cb03-c95b-4e86-9246-4230bebfed23", new DateTimeOffset(new DateTime(2024, 2, 5, 19, 14, 51, 82, DateTimeKind.Unspecified).AddTicks(5068), new TimeSpan(0, 3, 0, 0, 0)), "00447021-43b4-4f6b-ae93-c61960043cd6", 0, null }
                });
        }
    }
}
