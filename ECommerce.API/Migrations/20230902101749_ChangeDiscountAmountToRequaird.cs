using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class ChangeDiscountAmountToRequaird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DiscountAmount",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3bb1f7f4-cec2-46f8-90a9-419cdb61c426");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a34f3fb7-4b21-4957-ae76-a57b5d0fea36");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "ee4b7bdd-4ed0-422a-a53f-9da8f1ab1bf6");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "d585a3c9-e0f6-4ca8-9a07-d333c87cc02b", "AQAAAAEAACcQAAAAEIQNV9Eb4ecnf4ex7lGHIeWENffd116Ype8Llo0ePx67bvV+1x0XdipMkQbIULL9Ig==", new DateTime(2023, 9, 2, 13, 47, 48, 663, DateTimeKind.Local).AddTicks(6779) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DiscountAmount",
                table: "PurchaseOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
