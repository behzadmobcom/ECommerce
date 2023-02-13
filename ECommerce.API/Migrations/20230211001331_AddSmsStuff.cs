using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddSmsStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfirmCode",
                schema: "Security",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmCodeExpirationDate",
                schema: "Security",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "58d7bd9a-1623-47bc-8ec9-76f63705c445");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "47d26cee-dd36-430e-ba7d-e90eb02ec176");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "931db539-24d1-49b2-8911-cf6c8a9c488a");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "bdc28071-b5d9-4076-808f-99694f61b3a5", "AQAAAAEAACcQAAAAEKPgNF729oaK8t1pvQiiILzHzrSmenKAUAJhrplsGitl/zj9+cpvBF028feWaORFhg==", new DateTime(2023, 2, 11, 3, 43, 31, 302, DateTimeKind.Local).AddTicks(1983) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmCode",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConfirmCodeExpirationDate",
                schema: "Security",
                table: "Users");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c54b6260-a1c3-49dd-9009-52e6c0c72acc");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2f48e490-b9bd-415f-8138-e9243b561cdd");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "103dfbae-ff42-41ef-86da-e428b871a64f");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "4cedc9d2-f7e6-4bcd-850a-d18ee7e03128", "AQAAAAEAACcQAAAAEHVjuDHUMflY1WgCdRDCGsVfRowfJKuCvQwpp40++Voy48WjWdtVG5OR1+2a+CNchA==", new DateTime(2022, 11, 15, 8, 16, 37, 92, DateTimeKind.Local).AddTicks(5619) });
        }
    }
}
