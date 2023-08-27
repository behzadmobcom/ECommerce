using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class BannerToCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0a71a162-a33d-4ea0-a5bb-e400bbf19e91");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4c4fdfa1-4d0e-4fda-a480-b9e23569175a");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "f1a13921-c39d-42a2-99c8-83f948bef898");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "705b4cca-63fd-4857-9a91-36aff4d4efe3", "AQAAAAEAACcQAAAAEPwWxHM0t7VuCNBrC/9nimtcJdcHFsm+X01QHrriWTjr4vE9lTYT61UmKPdgMO4fbg==", new DateTime(2023, 8, 28, 0, 20, 54, 284, DateTimeKind.Local).AddTicks(6738) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Categories");

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
