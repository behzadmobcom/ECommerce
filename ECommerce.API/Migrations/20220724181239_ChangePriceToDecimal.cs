using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class ChangePriceToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "PurchaseOrders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "PurchaseOrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "SumPrice",
                table: "PurchaseOrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Prices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "bde9abd3-cbd2-4495-88a5-a4fcee16e944");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "10356bb2-3022-44e1-a3c8-554a820973c2");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "460c9cdf-e5f2-4877-aeff-7ec5dde98914");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "ace4137c-6b02-44c1-9538-82ecffba69e4", "AQAAAAEAACcQAAAAEAI2gzJgZw4Pn5DB7lJceleLyvcIVV+DWZXHYGSvJ65J6eUilDxgmwMz0hx8bb6cKw==", new DateTime(2022, 7, 24, 22, 42, 35, 804, DateTimeKind.Local).AddTicks(6854) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "UnitPrice",
                table: "PurchaseOrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "SumPrice",
                table: "PurchaseOrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Prices",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

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

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "CityId", "CompanyName", "ConcurrencyStamp", "CustomerCode", "CustomerCodeCustomer", "Email", "EmailConfirmed", "FatherName", "FirstName", "HolooCompanyId", "IsActive", "IsColleague", "IsConfirmedColleague", "IsFeeder", "IsHaveCustomerCode", "LastName", "LicensePath", "LockoutEnabled", "LockoutEnd", "Mobile", "NationalCode", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PicturePath", "RegisterDate", "SecurityStamp", "StateId", "TwoFactorEnabled", "UserName", "UserRoleId" },
                values: new object[] { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "cb2b60c2-c6f3-4ece-8193-de6b98624846", null, null, "sajjad.nazmi@gmail.com", true, null, "Sajjad", 1, true, false, false, false, false, "Nazmi", null, false, null, "No Mobile", null, "SAJJAD.NAZMI@GMAIL.COM", "SAJJADNAZMI", "AQAAAAEAACcQAAAAECCdgb1RhpplEgBBPNskCM5lQMrVyljo8TNtfSnCS+pnY+tqD+qgy3LB7QBM5gKFGA==", "09119394726", true, null, new DateTime(2022, 6, 8, 17, 54, 23, 418, DateTimeKind.Local).AddTicks(4225), "", null, false, "sajjadnazmi", 1 });
        }
    }
}
