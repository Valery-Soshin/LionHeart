using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "5d7beb25-e633-42fc-ac17-f12e3416ae74");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "883a89ec-5890-4692-9554-34ff03402e4e");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "cb7722fa-fcdb-4e16-a5b7-c1a8e758c87b");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "de65f730-8126-4f28-bfe2-8954d5fa14d2");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "ed53fce2-a53a-445f-bc04-eef93f9389e6");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "77393cfb-fb6f-4464-88fc-1fc1e71b731d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "239259e9-8195-40d5-bba5-5dadf0a0359e");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "a6c5f31b-8603-4fb7-821e-4796bdfb0d1c");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "products");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "personal_discount", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "239259e9-8195-40d5-bba5-5dadf0a0359e", 0, "bc25b354-a624-4e70-babc-b1e0a975d357", "admin", false, null, null, false, null, null, null, null, 0m, null, false, "4f1ab695-d08a-4070-ada2-bc0063b3e989", false, "admin" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "a6c5f31b-8603-4fb7-821e-4796bdfb0d1c", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "user_id" },
                values: new object[] { "77393cfb-fb6f-4464-88fc-1fc1e71b731d", "a6c5f31b-8603-4fb7-821e-4796bdfb0d1c", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", "239259e9-8195-40d5-bba5-5dadf0a0359e" });

            migrationBuilder.InsertData(
                table: "product_units",
                columns: new[] { "id", "created_at", "product_id", "sale_status" },
                values: new object[,]
                {
                    { "5d7beb25-e633-42fc-ac17-f12e3416ae74", new DateTimeOffset(new DateTime(2024, 2, 7, 20, 36, 43, 667, DateTimeKind.Unspecified).AddTicks(86), new TimeSpan(0, 3, 0, 0, 0)), "77393cfb-fb6f-4464-88fc-1fc1e71b731d", 0 },
                    { "883a89ec-5890-4692-9554-34ff03402e4e", new DateTimeOffset(new DateTime(2024, 2, 7, 20, 36, 43, 667, DateTimeKind.Unspecified).AddTicks(53), new TimeSpan(0, 3, 0, 0, 0)), "77393cfb-fb6f-4464-88fc-1fc1e71b731d", 0 },
                    { "cb7722fa-fcdb-4e16-a5b7-c1a8e758c87b", new DateTimeOffset(new DateTime(2024, 2, 7, 20, 36, 43, 667, DateTimeKind.Unspecified).AddTicks(36), new TimeSpan(0, 3, 0, 0, 0)), "77393cfb-fb6f-4464-88fc-1fc1e71b731d", 0 },
                    { "de65f730-8126-4f28-bfe2-8954d5fa14d2", new DateTimeOffset(new DateTime(2024, 2, 7, 20, 36, 43, 667, DateTimeKind.Unspecified).AddTicks(69), new TimeSpan(0, 3, 0, 0, 0)), "77393cfb-fb6f-4464-88fc-1fc1e71b731d", 0 },
                    { "ed53fce2-a53a-445f-bc04-eef93f9389e6", new DateTimeOffset(new DateTime(2024, 2, 7, 20, 36, 43, 666, DateTimeKind.Unspecified).AddTicks(9968), new TimeSpan(0, 3, 0, 0, 0)), "77393cfb-fb6f-4464-88fc-1fc1e71b731d", 0 }
                });
        }
    }
}
