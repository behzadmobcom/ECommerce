using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddArticleCodeToPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticleCode",
                table: "Prices",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArticleCodeCustomer",
                table: "Prices",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleCode",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "ArticleCodeCustomer",
                table: "Prices");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "341d7ad9-a9ad-47ca-8e5e-bce11e26e39a");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cebcce0f-4189-4cea-9873-8a15f7bfe284");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "6323fe9e-7e66-4e86-8085-d90ec708427e");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "29c5aad2-3807-4eba-ab63-1eca77381254", "AQAAAAEAACcQAAAAEPRTu9GdUZuU1oWYZhRQecgYn6j7TaJ8cKYP/730wbHThDl3xU74C2XOFNbCb8xIrw==", new DateTime(2022, 2, 20, 15, 35, 22, 664, DateTimeKind.Local).AddTicks(8836) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "b0242a38-ee83-4b93-8002-7c39f3f0328e", "AQAAAAEAACcQAAAAEGnf27HlZ5L9PUtfqGtorHc+woBhHyWSxj/3nmpjgp3UYnMLuqddFZtdEatn26Clsg==", new DateTime(2022, 2, 20, 15, 35, 22, 675, DateTimeKind.Local).AddTicks(1174) });
        }
    }
}
