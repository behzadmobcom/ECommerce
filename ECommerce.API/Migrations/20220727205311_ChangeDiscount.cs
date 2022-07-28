using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class ChangeDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Percent",
                table: "Discounts",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "MinOrder",
                table: "Discounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaxOrder",
                table: "Discounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaxAmount",
                table: "Discounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Discounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amount", "MaxAmount", "MaxOrder", "MinOrder", "Percent" },
                values: new object[] { null, null, null, null, null });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Percent",
                table: "Discounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinOrder",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaxOrder",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaxAmount",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amount", "MaxAmount", "MaxOrder", "MinOrder", "Percent" },
                values: new object[] { 0, 0, 0, 0, 0.0 });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c71deb62-09e4-4b07-b625-681169eb0d4f");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e9823fbd-41eb-4d3e-9c0f-b3f9e5ee4e33");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "a00ce929-bcfe-4abe-a463-9225b1b59251");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "78605a73-1e8f-474d-b291-a7c383c1d218", "AQAAAAEAACcQAAAAEM52QObDJJWLp9dxtdmPrF9G7LBOREnbxrsrhhQeSgMwBPuZNWyGh9z3v61GTJszCw==", new DateTime(2022, 7, 26, 15, 0, 32, 498, DateTimeKind.Local).AddTicks(5237) });
        }
    }
}
