using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class ChangeRfIdLenght : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefId",
                table: "Transactions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cf8ec58f-a63a-42ba-a475-08552f1a5a05");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b4f318bc-59ca-42ce-a757-46255e6264f5");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "0b9081cd-dd9f-47e7-97f1-0429e1f93aa2");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "68f6f7f8-274c-44e5-8ded-8933f6611a4d", "AQAAAAEAACcQAAAAEGmLTndiHB6cLmDIffPw9XrqIx61eSjkJMH2yTz/tX8EtNOfKS4cJQMZW3//Y8Dizw==", new DateTime(2022, 8, 29, 13, 51, 23, 271, DateTimeKind.Local).AddTicks(3444) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefId",
                table: "Transactions",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

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
        }
    }
}
