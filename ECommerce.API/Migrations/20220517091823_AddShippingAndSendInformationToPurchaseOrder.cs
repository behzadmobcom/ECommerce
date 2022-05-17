using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddShippingAndSendInformationToPurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SendInformationId",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "58d217b6-4ca3-40fa-8adf-9bbd6823c6ea");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8caea53e-6b81-46d2-9564-7cafbce7ae3c");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "2af944a9-2f4f-416e-89b9-6de71c78cfae");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "98d5efbf-2383-41a5-a484-c45b75ab2e2e", "AQAAAAEAACcQAAAAEKO9eIK21lIQpnvdJelIeC8wqwY0mw6Filr+KnDzPUyIuwNGc6arSOwBuCUp/dOJPg==", new DateTime(2022, 5, 17, 13, 48, 21, 74, DateTimeKind.Local).AddTicks(6025) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "9364510b-df41-4496-af80-59967296fab9", "AQAAAAEAACcQAAAAENGQ8bQ6dr6u7ycBjn+4hfnvwflw6LVryuW5KMEHhrs0l6qe+LLs5HsVenD0FynQIw==", new DateTime(2022, 5, 17, 13, 48, 21, 84, DateTimeKind.Local).AddTicks(6366) });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PaymentMethodId",
                table: "PurchaseOrders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SendInformationId",
                table: "PurchaseOrders",
                column: "SendInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_PaymentMethods_PaymentMethodId",
                table: "PurchaseOrders",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_SendInformation_SendInformationId",
                table: "PurchaseOrders",
                column: "SendInformationId",
                principalTable: "SendInformation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_PaymentMethods_PaymentMethodId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_SendInformation_SendInformationId",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_PaymentMethodId",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_SendInformationId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "SendInformationId",
                table: "PurchaseOrders");

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
        }
    }
}
