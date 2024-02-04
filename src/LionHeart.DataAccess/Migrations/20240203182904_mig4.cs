using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_details_product_details_product_detail_id",
                table: "order_details");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "d9946d62-f35e-46cd-ae23-2ab4eb723e36");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "cd3f58ce-53c6-4400-9514-428f2285f8ec");

            migrationBuilder.AlterColumn<string>(
                name: "product_detail_id",
                table: "order_details",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "cb52d9d6-b07d-478f-91eb-f2faed541300", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "f940ee90-4673-4099-ab2b-0b04b18f9046", "cb52d9d6-b07d-478f-91eb-f2faed541300", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });

            migrationBuilder.CreateIndex(
                name: "ix_order_details_order_id",
                table: "order_details",
                column: "order_id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_details_orders_order_id",
                table: "order_details",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_details_product_details_product_detail_id",
                table: "order_details",
                column: "product_detail_id",
                principalTable: "product_details",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_details_orders_order_id",
                table: "order_details");

            migrationBuilder.DropForeignKey(
                name: "fk_order_details_product_details_product_detail_id",
                table: "order_details");

            migrationBuilder.DropIndex(
                name: "ix_order_details_order_id",
                table: "order_details");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "f940ee90-4673-4099-ab2b-0b04b18f9046");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "cb52d9d6-b07d-478f-91eb-f2faed541300");

            migrationBuilder.AlterColumn<string>(
                name: "product_detail_id",
                table: "order_details",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "cd3f58ce-53c6-4400-9514-428f2285f8ec", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "d9946d62-f35e-46cd-ae23-2ab4eb723e36", "cd3f58ce-53c6-4400-9514-428f2285f8ec", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });

            migrationBuilder.AddForeignKey(
                name: "fk_order_details_product_details_product_detail_id",
                table: "order_details",
                column: "product_detail_id",
                principalTable: "product_details",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
