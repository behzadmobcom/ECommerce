using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddStarToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Star",
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
                value: "1001c56a-7634-4357-b3ad-506d40a3278f");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "55358219-8862-44ae-8e35-31024aa34a6d");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "d0924059-3d82-4198-b753-e63ddfeb87ef");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "ecc608b0-97f8-4db6-8644-f9c227484692", "AQAAAAEAACcQAAAAEIn6hpVH/6/0oDI7xVIxfYra1wiOREXBLvcV9LcXYNH1jhs4OuHknXf2ACxyWu9tQg==", new DateTime(2022, 5, 14, 18, 28, 4, 955, DateTimeKind.Local).AddTicks(7015) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "80b793ea-031d-454b-9517-a05b297fda92", "AQAAAAEAACcQAAAAEOvr+EBB/v15bsRQYvn6MDSAFWBcq9QUC36QkrWdUl0Urs4M7SfVRa202LawCqyzdQ==", new DateTime(2022, 5, 14, 18, 28, 4, 967, DateTimeKind.Local).AddTicks(3556) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Star",
                table: "Products");

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
    }
}
