using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddImageToBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_BlogId",
                table: "Images");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "52a44d87-2dd8-4db9-b650-198fe1bafb4e");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d7ed42ff-5ba6-424e-90ba-5e8545903534");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "34d7eda1-d2ec-4f2d-97b8-7abc6c0863d7");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "d0395af2-2ebf-4ce8-ac29-ad46e7e6dbfe", "AQAAAAEAACcQAAAAEJzr5AGwM+GFEvzbLUSfZDy2Qw1yAHC3xuoXruJxUrrKGWq1zKUvGYju2p1SW5KjzQ==", new DateTime(2022, 6, 8, 17, 54, 23, 388, DateTimeKind.Local).AddTicks(4645) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "cb2b60c2-c6f3-4ece-8193-de6b98624846", "AQAAAAEAACcQAAAAECCdgb1RhpplEgBBPNskCM5lQMrVyljo8TNtfSnCS+pnY+tqD+qgy3LB7QBM5gKFGA==", new DateTime(2022, 6, 8, 17, 54, 23, 418, DateTimeKind.Local).AddTicks(4225) });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_PriceId",
                table: "PurchaseOrderDetails",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_BlogId",
                table: "Images",
                column: "BlogId",
                unique: true,
                filter: "[BlogId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetails_Prices_PriceId",
                table: "PurchaseOrderDetails",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetails_Prices_PriceId",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderDetails_PriceId",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_Images_BlogId",
                table: "Images");

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
                name: "IX_Images_BlogId",
                table: "Images",
                column: "BlogId");
        }
    }
}
