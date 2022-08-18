using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class ChangeDicountFromProductToPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DiscountId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Prices",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d1f42b7f-05d9-4b54-9b17-7e52595900d6");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "61fd54b8-b76b-4a86-b6b6-3235c5d46188");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "3882dc70-9f79-427d-ade4-e2dcf3f6daa7");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "2417b50c-6f21-479d-936d-c080ad26b7d8", "AQAAAAEAACcQAAAAEAlqb6sDJEQd+xEsxLtXoAT2YQHhAGhwkNW06Y91zMtrxmJRgHj8LKUCpQoVRVuvSg==", new DateTime(2022, 8, 18, 23, 28, 48, 312, DateTimeKind.Local).AddTicks(9669) });

            migrationBuilder.CreateIndex(
                name: "IX_Prices_DiscountId",
                table: "Prices",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Discounts_DiscountId",
                table: "Prices",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Discounts_DiscountId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_DiscountId",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Prices");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "17120216-d681-4b84-aaf6-3cfdb8215b15");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a8ce949d-6bbc-487e-b429-bf3d42c11622");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "0541bda1-9327-4df2-b45e-52a321809480");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "87c223d6-5d40-416e-837a-be90c633130c", "AQAAAAEAACcQAAAAEGGJLHqTNJn3bJHV6Csj44rsM/SKMBbxSmpWirNSOw8I4LYcmcQbOYl9jL4hN92kdA==", new DateTime(2022, 8, 6, 18, 8, 44, 608, DateTimeKind.Local).AddTicks(1799) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DiscountId",
                table: "Products",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }
    }
}
