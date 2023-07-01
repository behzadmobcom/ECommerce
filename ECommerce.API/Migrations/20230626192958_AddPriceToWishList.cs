using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddPriceToWishList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Products_ProductId",
                table: "WishLists");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "WishLists",
                newName: "PriceId");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_ProductId",
                table: "WishLists",
                newName: "IX_WishLists_PriceId");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "94eeeb0a-2d32-4cea-9018-84a28e58d767");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "809d2afb-a25e-4741-8682-0a236ca58886");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "0b54c72c-e075-4412-a04f-548395eca176");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "549eee3f-3c27-44ae-9b25-759da2cd61b7", "AQAAAAEAACcQAAAAEF58pQdT/5kPUbGNXZFWhmZySglXeIRYapT3+cxcYvHVmMLvZ4hR1WCFfJGJJu2IUg==", new DateTime(2023, 6, 26, 22, 59, 57, 610, DateTimeKind.Local).AddTicks(7568) });

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Prices_PriceId",
                table: "WishLists",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Prices_PriceId",
                table: "WishLists");

            migrationBuilder.RenameColumn(
                name: "PriceId",
                table: "WishLists",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_PriceId",
                table: "WishLists",
                newName: "IX_WishLists_ProductId");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ff006c5a-16ae-431e-8381-e0461bf63f74");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "88d70c31-9414-4fd0-8e16-4da80804a8cf");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "e5641f25-5ce7-4e2c-98b2-9357e976f017");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "28668ff8-9057-447c-bd4c-f8cb3a2823a7", "AQAAAAEAACcQAAAAEFlWLcE1lF0Pdx7YyPD+nbetvJ9GJuz0bhcR1ENhu4Qf5Uv6XtsGYWGpMFqJq7+woA==", new DateTime(2023, 6, 17, 17, 33, 2, 880, DateTimeKind.Local).AddTicks(1976) });

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Products_ProductId",
                table: "WishLists",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
