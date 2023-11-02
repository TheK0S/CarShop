using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class CartAndOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopCartItem_Car_CarId",
                table: "ShopCartItem");

            migrationBuilder.DropIndex(
                name: "IX_ShopCartItem_CarId",
                table: "ShopCartItem");

            migrationBuilder.AlterColumn<int>(
                name: "ShopCartId",
                table: "ShopCartItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "ShopCartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ShopCartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCart", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopCartItem_OrderId",
                table: "ShopCartItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopCartItem_ShopCartId",
                table: "ShopCartItem",
                column: "ShopCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCartItem_Order_OrderId",
                table: "ShopCartItem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCartItem_ShopCart_ShopCartId",
                table: "ShopCartItem",
                column: "ShopCartId",
                principalTable: "ShopCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopCartItem_Order_OrderId",
                table: "ShopCartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopCartItem_ShopCart_ShopCartId",
                table: "ShopCartItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ShopCart");

            migrationBuilder.DropIndex(
                name: "IX_ShopCartItem_OrderId",
                table: "ShopCartItem");

            migrationBuilder.DropIndex(
                name: "IX_ShopCartItem_ShopCartId",
                table: "ShopCartItem");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "ShopCartItem");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ShopCartItem");

            migrationBuilder.AlterColumn<string>(
                name: "ShopCartId",
                table: "ShopCartItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ShopCartItem_CarId",
                table: "ShopCartItem",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCartItem_Car_CarId",
                table: "ShopCartItem",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
