using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class ExistToPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ArticleCodeCustomer",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Exist",
                table: "Products");

            migrationBuilder.AddColumn<double>(
                name: "Exist",
                table: "Prices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Prices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d513e9e2-ffd7-42de-a75b-999b4ca706cb");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "932c2465-f6dc-4673-b8f7-32173c1e1500");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "381237c7-9360-4bad-aaa5-0210ba746beb");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "5c4843e6-95f7-41d6-ab3b-346a6c2a635c", "AQAAAAEAACcQAAAAEJzP/goxXo7AVclEZ+HvXs2JhoLayUWxE9YIZLVQePREZCgk0bh2e+kYoAEokTXMCg==", new DateTime(2022, 3, 19, 21, 28, 45, 135, DateTimeKind.Local).AddTicks(1272) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "b86037b1-74b9-4c60-8d68-965ef02758ae", "AQAAAAEAACcQAAAAEPTuFlVqqBeSdGDIMzzxfr4D1PodQBNUhts5pTRXccpozk4RpkFs/BC0SBA3k+zklA==", new DateTime(2022, 3, 19, 21, 28, 45, 148, DateTimeKind.Local).AddTicks(189) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exist",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Prices");

            migrationBuilder.AddColumn<string>(
                name: "ArticleCode",
                table: "Products",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArticleCodeCustomer",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Exist",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0666d6d1-3800-4a52-beee-5a05ce2ad00e");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "027301de-0725-4756-bb0a-fde835fa20b4");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "990d9836-b9dd-48df-b675-094cf589858c");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "4289fb90-f9c6-40ef-affc-d0bb30b4152d", "AQAAAAEAACcQAAAAEJCkclavbTS7UeVqqfcpz1KtGN/Wmv6lZc7CZ/VqmTnatFD6YURkOdOJqHzgrntAkQ==", new DateTime(2022, 3, 19, 17, 53, 54, 920, DateTimeKind.Local).AddTicks(4671) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "1e9e5392-97d9-4397-99b6-e4456bf688b2", "AQAAAAEAACcQAAAAEF2S9/Hsqvtv1kVix5XAh/h9DZEsxp5LiaRuRmLWazaSqAhR1Exy/DNaB+nUlt5adQ==", new DateTime(2022, 3, 19, 17, 53, 54, 937, DateTimeKind.Local).AddTicks(5626) });
        }
    }
}
