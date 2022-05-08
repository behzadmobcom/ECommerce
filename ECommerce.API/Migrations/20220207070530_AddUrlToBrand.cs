using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddUrlToBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8596b922-7c03-4c0f-a5d3-c3b09c3d0bb4");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "48835cc0-19a0-4c61-9ef4-220c1d721386");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "5a2fdf24-8e8e-4971-9cca-64b98195e7e4");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "8ecb2f65-02a7-4442-9aae-f79eeb7c40f3", "AQAAAAEAACcQAAAAEMKHdHUWcD7hPPh18sBIQ2paJMKgRrUXyM3qQRuPmmvGPfxBrWrwFL6deO+/KNtPRw==", new DateTime(2022, 2, 7, 10, 35, 27, 821, DateTimeKind.Local).AddTicks(851) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "47867394-64dd-4850-9076-fa8b74505f75", "AQAAAAEAACcQAAAAEHEvdSDjzSRQwBFPIq6O41p3SmSLVcL01cZh4tcoMHg46R4h1R5ve/A8AU7g0w+Enw==", new DateTime(2022, 2, 7, 10, 35, 27, 832, DateTimeKind.Local).AddTicks(415) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Brands");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "14c87bba-96d8-4de2-86b9-8061925a3ade");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cb6c3deb-cf57-4331-87a0-6e5a29c006da");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "3b1ff526-876a-4c7c-972e-fdcf5b2c53b2");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "488b3fc6-c41e-4a51-92eb-fd3cf272f844", "AQAAAAEAACcQAAAAEMPCN8+HW0kYeNRm7KuURaYYk676fiJ2ZeYxMM38W2xND/1plAoLxk1oW+MRNjeZpA==", new DateTime(2022, 2, 6, 9, 2, 35, 9, DateTimeKind.Local).AddTicks(8977) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "57dc13a0-e9a5-4a4b-a526-c35f31de0a97", "AQAAAAEAACcQAAAAEO/MVtvpAPe23f7Mu2qjKmL7jQXZhy3BYR0Os9KHkYIXDGAXnWAbipYiGMhCALWIsw==", new DateTime(2022, 2, 6, 9, 2, 35, 20, DateTimeKind.Local).AddTicks(5815) });
        }
    }
}
