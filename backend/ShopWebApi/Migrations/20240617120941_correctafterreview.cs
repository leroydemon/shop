using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopWebApi.Migrations
{
    /// <inheritdoc />
    public partial class correctafterreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "StorageId",
                table: "Products",
                newName: "ProductStorageId");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ProductStorageId",
                table: "Carts",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductStorageId",
                table: "Products",
                newName: "StorageId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "CartId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Carts",
                newName: "ProductStorageId");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "Carts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
