using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class RootEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "PaymentMethods",
                type: "nvarchar(26)",
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Contacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Colors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Colors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Colors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Colors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 8, 12, 13, 33, 364, DateTimeKind.Local).AddTicks(628), new DateTime(2023, 10, 8, 12, 13, 33, 364, DateTimeKind.Local).AddTicks(628) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7ff8ea74-6727-46a0-942c-6fa51320ce9e");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e31f2147-1246-4eac-b1f9-12f15db094ba");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "140d6a45-0fb3-4a78-87fe-8ea1b1da0c5a");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "67edd8d6-ed34-4514-b657-6e70c47e8cd9", "AQAAAAEAACcQAAAAEE2mWKYiqXdpRpRpyWOImFHcnFfiAJel+V6Ld55FLgkelyEGa+084TTJLjbZ09MnuA==", new DateTime(2023, 10, 8, 12, 13, 33, 362, DateTimeKind.Local).AddTicks(8463) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Colors");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "PaymentMethods",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(26)",
                oldMaxLength: 26);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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
    }
}
