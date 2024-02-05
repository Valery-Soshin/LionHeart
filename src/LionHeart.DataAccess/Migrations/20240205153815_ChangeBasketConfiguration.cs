using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBasketConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "1fb6570e-5348-4e44-927d-baebb0f4cca1");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "4e786eb9-bd56-4ead-9d2b-fb97eec138c9");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "5e9c128a-3dae-4fc9-84de-516ceb5784e9");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "98bf4c61-bfdc-49c8-ba6b-7e0176425c33");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "c1fe1027-018a-493d-b9f7-4ffb83dd152d");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "3faadc4d-dadb-42e3-ac5d-1f97b5cd3fdb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "52ef5ad2-a23c-46d2-853d-0dd5731da7ac");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "8b3e1ffe-9f69-4908-bc86-3f9b48a6e319");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "personal_discount", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "410abfa6-3757-45d0-9fdb-bdcf255baef0", 0, "2cc84193-5a30-4c95-b996-6cf9a292ff9c", "admin", false, null, null, false, null, null, null, null, 0m, null, false, "419f251b-6d70-4d96-b545-0e580a48c4e6", false, "admin" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "53cd8df2-2d4e-4faf-80bd-156368bb33cf", "Одежда" });

            migrationBuilder.InsertData(
                table: "product_units",
                columns: new[] { "id", "created_at", "product_id", "sale_status", "sold_on" },
                values: new object[,]
                {
                    { "3132134d-5e7b-4027-8813-c10f1c5f84a6", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 38, 15, 356, DateTimeKind.Unspecified).AddTicks(9294), new TimeSpan(0, 3, 0, 0, 0)), "ac2bad60-b8f7-4bf0-b4f4-bb5c8d341df5", 0, null },
                    { "449440e7-07b2-4428-a2b3-41ba4f073be6", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 38, 15, 356, DateTimeKind.Unspecified).AddTicks(9408), new TimeSpan(0, 3, 0, 0, 0)), "ac2bad60-b8f7-4bf0-b4f4-bb5c8d341df5", 0, null },
                    { "4cf3b34e-47db-4b77-a9d7-f5196af28fdd", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 38, 15, 356, DateTimeKind.Unspecified).AddTicks(9362), new TimeSpan(0, 3, 0, 0, 0)), "ac2bad60-b8f7-4bf0-b4f4-bb5c8d341df5", 0, null },
                    { "7693aa6c-60f3-4489-80c5-a01ebabc8467", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 38, 15, 356, DateTimeKind.Unspecified).AddTicks(9442), new TimeSpan(0, 3, 0, 0, 0)), "ac2bad60-b8f7-4bf0-b4f4-bb5c8d341df5", 0, null },
                    { "929ae810-83a4-41cc-9b5c-fc833d7ce771", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 38, 15, 356, DateTimeKind.Unspecified).AddTicks(9425), new TimeSpan(0, 3, 0, 0, 0)), "ac2bad60-b8f7-4bf0-b4f4-bb5c8d341df5", 0, null }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "user_id" },
                values: new object[] { "ac2bad60-b8f7-4bf0-b4f4-bb5c8d341df5", "53cd8df2-2d4e-4faf-80bd-156368bb33cf", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", "410abfa6-3757-45d0-9fdb-bdcf255baef0" });

            migrationBuilder.CreateIndex(
                name: "ix_baskets_user_id",
                table: "baskets",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_baskets_users_user_id",
                table: "baskets",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_baskets_users_user_id",
                table: "baskets");

            migrationBuilder.DropIndex(
                name: "ix_baskets_user_id",
                table: "baskets");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "3132134d-5e7b-4027-8813-c10f1c5f84a6");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "449440e7-07b2-4428-a2b3-41ba4f073be6");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "4cf3b34e-47db-4b77-a9d7-f5196af28fdd");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "7693aa6c-60f3-4489-80c5-a01ebabc8467");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "929ae810-83a4-41cc-9b5c-fc833d7ce771");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "ac2bad60-b8f7-4bf0-b4f4-bb5c8d341df5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "410abfa6-3757-45d0-9fdb-bdcf255baef0");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "53cd8df2-2d4e-4faf-80bd-156368bb33cf");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "personal_discount", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "52ef5ad2-a23c-46d2-853d-0dd5731da7ac", 0, "6d7b4546-189b-4434-9db1-7fb3fe7db2c6", "admin", false, null, null, false, null, null, null, null, 0m, null, false, "c578c46d-43f5-4ea5-9f92-8f3394b30ae1", false, "admin" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "8b3e1ffe-9f69-4908-bc86-3f9b48a6e319", "Одежда" });

            migrationBuilder.InsertData(
                table: "product_units",
                columns: new[] { "id", "created_at", "product_id", "sale_status", "sold_on" },
                values: new object[,]
                {
                    { "1fb6570e-5348-4e44-927d-baebb0f4cca1", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 27, 16, 997, DateTimeKind.Unspecified).AddTicks(2807), new TimeSpan(0, 3, 0, 0, 0)), "3faadc4d-dadb-42e3-ac5d-1f97b5cd3fdb", 0, null },
                    { "4e786eb9-bd56-4ead-9d2b-fb97eec138c9", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 27, 16, 997, DateTimeKind.Unspecified).AddTicks(2943), new TimeSpan(0, 3, 0, 0, 0)), "3faadc4d-dadb-42e3-ac5d-1f97b5cd3fdb", 0, null },
                    { "5e9c128a-3dae-4fc9-84de-516ceb5784e9", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 27, 16, 997, DateTimeKind.Unspecified).AddTicks(2890), new TimeSpan(0, 3, 0, 0, 0)), "3faadc4d-dadb-42e3-ac5d-1f97b5cd3fdb", 0, null },
                    { "98bf4c61-bfdc-49c8-ba6b-7e0176425c33", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 27, 16, 997, DateTimeKind.Unspecified).AddTicks(2966), new TimeSpan(0, 3, 0, 0, 0)), "3faadc4d-dadb-42e3-ac5d-1f97b5cd3fdb", 0, null },
                    { "c1fe1027-018a-493d-b9f7-4ffb83dd152d", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 27, 16, 997, DateTimeKind.Unspecified).AddTicks(2870), new TimeSpan(0, 3, 0, 0, 0)), "3faadc4d-dadb-42e3-ac5d-1f97b5cd3fdb", 0, null }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "user_id" },
                values: new object[] { "3faadc4d-dadb-42e3-ac5d-1f97b5cd3fdb", "8b3e1ffe-9f69-4908-bc86-3f9b48a6e319", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", "52ef5ad2-a23c-46d2-853d-0dd5731da7ac" });
        }
    }
}
