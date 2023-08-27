using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddProductsToDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CouponQty",
                table: "Discounts",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "98e4ad5c-ee23-46eb-86b2-5cb7dbf7ae8e");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f7b9b938-71d2-417f-8906-6f4f87a77143");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "90fc3be5-7e4d-4e48-82cf-671c150c8b34");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "4709711a-d839-433c-b8c2-af73559ed391", "AQAAAAEAACcQAAAAEIe/roljtMlGECc/zVGYI7nWJfnRcdWOBUx6fdOPYIXqMiE/qSnSOdOmuWTFEqRNaA==", new DateTime(2023, 8, 13, 19, 30, 49, 187, DateTimeKind.Local).AddTicks(6636) });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CouponQty",
                table: "Discounts");

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
        }
    }
}
