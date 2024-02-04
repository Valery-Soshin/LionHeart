using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameProductDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_details_product_details_product_detail_id",
                table: "order_details");

            migrationBuilder.DropTable(
                name: "product_details");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "f940ee90-4673-4099-ab2b-0b04b18f9046");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "cb52d9d6-b07d-478f-91eb-f2faed541300");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "orders",
                newName: "total_price");

            migrationBuilder.RenameColumn(
                name: "product_detail_id",
                table: "order_details",
                newName: "product_unit_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_details_product_detail_id",
                table: "order_details",
                newName: "ix_order_details_product_unit_id");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "marked_products",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "product_units",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    product_id = table.Column<string>(type: "text", nullable: false),
                    sale_status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    sold_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_units", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "5407c651-969e-4de8-8b6e-16ccd1db9445", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "752b8e4d-63e2-44e8-b71f-9bb2c3deb0d1", "5407c651-969e-4de8-8b6e-16ccd1db9445", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });

            migrationBuilder.CreateIndex(
                name: "ix_marked_products_user_id",
                table: "marked_products",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_marked_products_users_user_id",
                table: "marked_products",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_details_product_units_product_unit_id",
                table: "order_details",
                column: "product_unit_id",
                principalTable: "product_units",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_marked_products_users_user_id",
                table: "marked_products");

            migrationBuilder.DropForeignKey(
                name: "fk_order_details_product_units_product_unit_id",
                table: "order_details");

            migrationBuilder.DropTable(
                name: "product_units");

            migrationBuilder.DropIndex(
                name: "ix_marked_products_user_id",
                table: "marked_products");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "752b8e4d-63e2-44e8-b71f-9bb2c3deb0d1");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "5407c651-969e-4de8-8b6e-16ccd1db9445");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "marked_products");

            migrationBuilder.RenameColumn(
                name: "total_price",
                table: "orders",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "product_unit_id",
                table: "order_details",
                newName: "product_detail_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_details_product_unit_id",
                table: "order_details",
                newName: "ix_order_details_product_detail_id");

            migrationBuilder.CreateTable(
                name: "product_details",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    product_id = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    sale_status = table.Column<int>(type: "integer", nullable: false),
                    sold_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_details_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "cb52d9d6-b07d-478f-91eb-f2faed541300", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "f940ee90-4673-4099-ab2b-0b04b18f9046", "cb52d9d6-b07d-478f-91eb-f2faed541300", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });

            migrationBuilder.CreateIndex(
                name: "ix_product_details_product_id",
                table: "product_details",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_details_product_details_product_detail_id",
                table: "order_details",
                column: "product_detail_id",
                principalTable: "product_details",
                principalColumn: "id");
        }
    }
}
