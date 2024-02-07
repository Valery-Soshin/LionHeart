using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AlterBasketModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "marked_products");

            migrationBuilder.DropTable(
                name: "baskets");

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

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "product_units",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "order_details",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "feedbacks",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "categories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.CreateTable(
                name: "basket_entries",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    product_id = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_basket_entries", x => x.id);
                    table.ForeignKey(
                        name: "fk_basket_entries_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_basket_entries_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "favorite_products",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    product_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_favorite_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_favorite_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_basket_entries_product_id",
                table: "basket_entries",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_basket_entries_user_id",
                table: "basket_entries",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_favorite_products_product_id",
                table: "favorite_products",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basket_entries");

            migrationBuilder.DropTable(
                name: "favorite_products");

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

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "products",
                type: "text",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "product_units",
                type: "text",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "order_details",
                type: "text",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "feedbacks",
                type: "text",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "categories",
                type: "text",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "baskets",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    basket_total_price = table.Column<decimal>(type: "numeric", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_baskets", x => x.id);
                    table.ForeignKey(
                        name: "fk_baskets_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "marked_products",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    product_id = table.Column<string>(type: "text", nullable: false),
                    discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    basket_id = table.Column<string>(type: "text", nullable: true),
                    products_total_price = table.Column<int>(type: "integer", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_marked_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_marked_products_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_marked_products_baskets_basket_id",
                        column: x => x.basket_id,
                        principalTable: "baskets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_marked_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_baskets_user_id",
                table: "baskets",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_marked_products_basket_id",
                table: "marked_products",
                column: "basket_id");

            migrationBuilder.CreateIndex(
                name: "ix_marked_products_product_id",
                table: "marked_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_marked_products_user_id",
                table: "marked_products",
                column: "user_id");
        }
    }
}
