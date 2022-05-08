using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Grade",
                table: "Prices",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f5815845-c786-4620-9ef1-ac364c6f7a41");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "912aad54-23d8-40f3-8c04-95fe122bf185");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "42cf3271-fca8-4973-9cca-3b4d7395e6bb");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "4a309c5a-bf19-4e70-a24b-2c0840a00110", "AQAAAAEAACcQAAAAEAVwz3sMRRkhjc6SOUw3/5mMnkrQU/UonZEcbUVzd5UIzKyIhCNCUZUlQKZ2OsaN5A==", new DateTime(2022, 4, 18, 16, 31, 11, 305, DateTimeKind.Local).AddTicks(6996) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "b1fac607-d425-4cb9-9c24-a9b53aad3f49", "AQAAAAEAACcQAAAAEKB1xoydpQMnHK1nDiCZloT9qM9SI4ie1bbF8ESm2N9exxsg21ARQQ4Kg55h4/Ilcg==", new DateTime(2022, 4, 18, 16, 31, 11, 325, DateTimeKind.Local).AddTicks(3234) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Prices");

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
    }
}
