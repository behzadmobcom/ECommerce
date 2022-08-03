using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddReviewToProduct : Migration
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
                value: "57fc370e-bb12-4dd8-901c-6fbc5f95b238");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c86df0b0-a1b8-4f5a-ace0-5cfad366054c");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "d54458a0-0b3b-4767-9fd4-7b299e6fdc4e");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "91ee8040-e4af-47da-9554-b18833c23529", "AQAAAAEAACcQAAAAEDFPmvT9fwSGl5ascpocwqbVmUu0sgCnxl0m2F6SpoyEqrzTjo0ICnVYmmBJoVWifg==", new DateTime(2022, 8, 3, 20, 15, 18, 50, DateTimeKind.Local).AddTicks(3944) });
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
