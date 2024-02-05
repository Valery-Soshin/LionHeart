using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductUnitConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { "afe8a795-a9b1-47ce-a9cd-a6fcd6987635", 0, "2f42c98c-e36c-412b-abd5-2ad552f43145", "admin", false, null, null, false, null, null, null, null, 0m, null, false, "cffc16ee-c5d9-4b7c-a227-2424fc97b106", false, "admin" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "7f6b5988-1023-46e2-add8-80b463faae82", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "user_id" },
                values: new object[] { "9cdf3f24-b028-486a-8b1f-dc58953122fe", "7f6b5988-1023-46e2-add8-80b463faae82", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", "afe8a795-a9b1-47ce-a9cd-a6fcd6987635" });

            migrationBuilder.InsertData(
                table: "product_units",
                columns: new[] { "id", "created_at", "product_id", "sale_status", "sold_on" },
                values: new object[,]
                {
                    { "172843b0-840b-474a-a39b-1e4b8458cf48", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 44, 18, 158, DateTimeKind.Unspecified).AddTicks(5197), new TimeSpan(0, 3, 0, 0, 0)), "9cdf3f24-b028-486a-8b1f-dc58953122fe", 0, null },
                    { "38969b42-9c5c-4332-b932-08c7775bbc23", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 44, 18, 158, DateTimeKind.Unspecified).AddTicks(5145), new TimeSpan(0, 3, 0, 0, 0)), "9cdf3f24-b028-486a-8b1f-dc58953122fe", 0, null },
                    { "669664ba-c176-4025-8605-4344305a8759", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 44, 18, 158, DateTimeKind.Unspecified).AddTicks(5081), new TimeSpan(0, 3, 0, 0, 0)), "9cdf3f24-b028-486a-8b1f-dc58953122fe", 0, null },
                    { "6ac3d2ed-f50c-4f51-8396-506f2823e0ca", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 44, 18, 158, DateTimeKind.Unspecified).AddTicks(5181), new TimeSpan(0, 3, 0, 0, 0)), "9cdf3f24-b028-486a-8b1f-dc58953122fe", 0, null },
                    { "7b540209-1e53-4863-97d6-927540a09f7e", new DateTimeOffset(new DateTime(2024, 2, 5, 18, 44, 18, 158, DateTimeKind.Unspecified).AddTicks(5162), new TimeSpan(0, 3, 0, 0, 0)), "9cdf3f24-b028-486a-8b1f-dc58953122fe", 0, null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_units_product_id",
                table: "product_units",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_units_products_product_id",
                table: "product_units",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_units_products_product_id",
                table: "product_units");

            migrationBuilder.DropIndex(
                name: "ix_product_units_product_id",
                table: "product_units");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "172843b0-840b-474a-a39b-1e4b8458cf48");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "38969b42-9c5c-4332-b932-08c7775bbc23");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "669664ba-c176-4025-8605-4344305a8759");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "6ac3d2ed-f50c-4f51-8396-506f2823e0ca");

            migrationBuilder.DeleteData(
                table: "product_units",
                keyColumn: "id",
                keyValue: "7b540209-1e53-4863-97d6-927540a09f7e");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "9cdf3f24-b028-486a-8b1f-dc58953122fe");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: "afe8a795-a9b1-47ce-a9cd-a6fcd6987635");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "7f6b5988-1023-46e2-add8-80b463faae82");

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
        }
    }
}
