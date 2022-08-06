using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class OrderIdInPurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PaymentId",
                table: "Transactions",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "PurchaseOrders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "17120216-d681-4b84-aaf6-3cfdb8215b15");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a8ce949d-6bbc-487e-b429-bf3d42c11622");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "0541bda1-9327-4df2-b45e-52a321809480");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "87c223d6-5d40-416e-837a-be90c633130c", "AQAAAAEAACcQAAAAEGGJLHqTNJn3bJHV6Csj44rsM/SKMBbxSmpWirNSOw8I4LYcmcQbOYl9jL4hN92kdA==", new DateTime(2022, 8, 6, 18, 8, 44, 608, DateTimeKind.Local).AddTicks(1799) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "PurchaseOrders");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "57fc370e-bb12-4dd8-901c-6fbc5f95b238");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c86df0b0-a1b8-4f5a-ace0-5cfad366054c");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "d54458a0-0b3b-4767-9fd4-7b299e6fdc4e");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "91ee8040-e4af-47da-9554-b18833c23529", "AQAAAAEAACcQAAAAEDFPmvT9fwSGl5ascpocwqbVmUu0sgCnxl0m2F6SpoyEqrzTjo0ICnVYmmBJoVWifg==", new DateTime(2022, 8, 3, 20, 15, 18, 50, DateTimeKind.Local).AddTicks(3944) });
        }
    }
}
