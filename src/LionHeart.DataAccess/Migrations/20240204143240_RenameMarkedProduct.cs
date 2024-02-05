using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LionHeart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameMarkedProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_marked_product_asp_net_users_user_id",
                table: "marked_product");

            migrationBuilder.DropForeignKey(
                name: "fk_marked_product_baskets_basket_id",
                table: "marked_product");

            migrationBuilder.DropForeignKey(
                name: "fk_marked_product_products_product_id",
                table: "marked_product");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_marked_product_customer_id_product_id",
                table: "marked_product");

            migrationBuilder.DropPrimaryKey(
                name: "pk_marked_product",
                table: "marked_product");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "a5f42d8f-0a27-4d7f-8483-48791e511f4c");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "1d4b390e-d8a0-4031-a921-e3c10bc3ff18");

            migrationBuilder.RenameTable(
                name: "marked_product",
                newName: "marked_products");

            migrationBuilder.RenameIndex(
                name: "ix_marked_product_user_id",
                table: "marked_products",
                newName: "ix_marked_products_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_marked_product_product_id",
                table: "marked_products",
                newName: "ix_marked_products_product_id");

            migrationBuilder.RenameIndex(
                name: "ix_marked_product_basket_id",
                table: "marked_products",
                newName: "ix_marked_products_basket_id");

            migrationBuilder.AddUniqueConstraint(
                name: "ak_marked_products_customer_id_product_id",
                table: "marked_products",
                columns: new[] { "customer_id", "product_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_marked_products",
                table: "marked_products",
                column: "id");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "d262e3d3-5966-4083-a3c1-63e1f942dbb4", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "1f8d43b1-0ace-40b2-af2e-f1b9f99c90be", "d262e3d3-5966-4083-a3c1-63e1f942dbb4", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });

            migrationBuilder.AddForeignKey(
                name: "fk_marked_products_asp_net_users_user_id",
                table: "marked_products",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_marked_products_baskets_basket_id",
                table: "marked_products",
                column: "basket_id",
                principalTable: "baskets",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_marked_products_products_product_id",
                table: "marked_products",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_marked_products_asp_net_users_user_id",
                table: "marked_products");

            migrationBuilder.DropForeignKey(
                name: "fk_marked_products_baskets_basket_id",
                table: "marked_products");

            migrationBuilder.DropForeignKey(
                name: "fk_marked_products_products_product_id",
                table: "marked_products");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_marked_products_customer_id_product_id",
                table: "marked_products");

            migrationBuilder.DropPrimaryKey(
                name: "pk_marked_products",
                table: "marked_products");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "id",
                keyValue: "1f8d43b1-0ace-40b2-af2e-f1b9f99c90be");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: "d262e3d3-5966-4083-a3c1-63e1f942dbb4");

            migrationBuilder.RenameTable(
                name: "marked_products",
                newName: "marked_product");

            migrationBuilder.RenameIndex(
                name: "ix_marked_products_user_id",
                table: "marked_product",
                newName: "ix_marked_product_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_marked_products_product_id",
                table: "marked_product",
                newName: "ix_marked_product_product_id");

            migrationBuilder.RenameIndex(
                name: "ix_marked_products_basket_id",
                table: "marked_product",
                newName: "ix_marked_product_basket_id");

            migrationBuilder.AddUniqueConstraint(
                name: "ak_marked_product_customer_id_product_id",
                table: "marked_product",
                columns: new[] { "customer_id", "product_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_marked_product",
                table: "marked_product",
                column: "id");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { "1d4b390e-d8a0-4031-a921-e3c10bc3ff18", "Одежда" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "description", "name", "price", "quantity", "specifications", "supplier_id" },
                values: new object[] { "a5f42d8f-0a27-4d7f-8483-48791e511f4c", "1d4b390e-d8a0-4031-a921-e3c10bc3ff18", "Красивая и удобная футболка", "Футболка", 1250m, 1, "Размер - XXL", null });

            migrationBuilder.AddForeignKey(
                name: "fk_marked_product_asp_net_users_user_id",
                table: "marked_product",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_marked_product_baskets_basket_id",
                table: "marked_product",
                column: "basket_id",
                principalTable: "baskets",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_marked_product_products_product_id",
                table: "marked_product",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
