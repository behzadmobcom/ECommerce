using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class IsShowInIndexPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShowInIndexPage",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShowInIndexPage",
                table: "Products");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "bde9abd3-cbd2-4495-88a5-a4fcee16e944");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "10356bb2-3022-44e1-a3c8-554a820973c2");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "460c9cdf-e5f2-4877-aeff-7ec5dde98914");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "ace4137c-6b02-44c1-9538-82ecffba69e4", "AQAAAAEAACcQAAAAEAI2gzJgZw4Pn5DB7lJceleLyvcIVV+DWZXHYGSvJ65J6eUilDxgmwMz0hx8bb6cKw==", new DateTime(2022, 7, 24, 22, 42, 35, 804, DateTimeKind.Local).AddTicks(6854) });
        }
    }
}
