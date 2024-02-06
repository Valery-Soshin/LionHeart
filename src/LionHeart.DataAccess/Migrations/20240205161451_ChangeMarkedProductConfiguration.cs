using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMarkedProductConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "ak_marked_products_user_id_product_id",
                table: "marked_products");

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

            migrationBuilder.CreateIndex(
                name: "ix_marked_products_user_id",
                table: "marked_products",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_marked_products_user_id",
                table: "marked_products");

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

            migrationBuilder.AddUniqueConstraint(
                name: "ak_marked_products_user_id_product_id",
                table: "marked_products",
                columns: new[] { "user_id", "product_id" });

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
        }
    }
}
