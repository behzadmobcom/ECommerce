using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f7e58a16-f5ef-42e8-a525-7b82951596cb");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e3d75be8-afbf-45f8-964d-499c7de10367");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "980189f0-fa21-4329-974c-d1b3a6c84f41");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "3ed731c6-5fb0-4a6f-aa2d-2ec4cce1bfab", "AQAAAAEAACcQAAAAEOoRR10B3b+Vuveys6B0OqQic4Fr0efOMx9jW48jTEkFv21YWD9Oy8KDVH4gMlHoVg==", new DateTime(2022, 8, 3, 20, 16, 1, 924, DateTimeKind.Local).AddTicks(5713) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Review",
                table: "Products");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ce4d968b-0fb0-42c1-a281-6b4a35f9a2db");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4d0031de-fe03-44c6-882d-fc5fcf8d198b");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "d69a2f44-e3cd-412c-9830-60783aae4635");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "84d639a2-aa50-4a30-a1e1-18b59e54a02d", "AQAAAAEAACcQAAAAEF1PSzSnem8PbDeR3VIGZwEGDpnoiGmmJdgQfoDjdzEjaoxvbHATpFNMzm0abX9bww==", new DateTime(2022, 7, 28, 1, 23, 8, 594, DateTimeKind.Local).AddTicks(2919) });
        }
    }
}
