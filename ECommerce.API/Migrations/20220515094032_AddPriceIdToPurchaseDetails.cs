using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddPriceIdToPurchaseDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Users_UserId",
                table: "PurchaseOrders");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "PurchaseOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0125fa97-be52-4082-94d9-d0875ecec70d");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "54acf78b-d848-42a6-a352-5baf38b296f7");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "8fbc5a0c-03c8-4cfb-8eee-994e8217ec4d");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "d68c98e4-db93-4c97-8f04-4d287f94461e", "AQAAAAEAACcQAAAAEBefOGmJI+dbTy/UARLblqvzI8tHKIsNwTbtJS1xZoX4jXGKVGZC2lzS/ZLInBVcrw==", new DateTime(2022, 5, 15, 14, 10, 30, 380, DateTimeKind.Local).AddTicks(4626) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "0c4a72d5-a98d-4ee1-8ce6-68b1643816a8", "AQAAAAEAACcQAAAAELmcmu1Ln3VCKd7GgpK5PRrVYcI3Ado1kUg0tEY4jWuNhXJM2iZ3QO0ebr7ZEcBt5g==", new DateTime(2022, 5, 15, 14, 10, 30, 391, DateTimeKind.Local).AddTicks(692) });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Users_UserId",
                table: "PurchaseOrders",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Users_UserId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "PurchaseOrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PurchaseOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Users_UserId",
                table: "PurchaseOrders",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
