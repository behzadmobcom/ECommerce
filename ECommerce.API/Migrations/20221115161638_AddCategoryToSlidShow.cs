using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddCategoryToSlidShow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlideShows_Products_ProductId",
                table: "SlideShows");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "SlideShows",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "SlideShows",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_SlideShows_CategoryId",
                table: "SlideShows",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SlideShows_Categories_CategoryId",
                table: "SlideShows",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SlideShows_Products_ProductId",
                table: "SlideShows",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlideShows_Categories_CategoryId",
                table: "SlideShows");

            migrationBuilder.DropForeignKey(
                name: "FK_SlideShows_Products_ProductId",
                table: "SlideShows");

            migrationBuilder.DropIndex(
                name: "IX_SlideShows_CategoryId",
                table: "SlideShows");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "SlideShows");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "SlideShows",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cf8ec58f-a63a-42ba-a475-08552f1a5a05");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b4f318bc-59ca-42ce-a757-46255e6264f5");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "0b9081cd-dd9f-47e7-97f1-0429e1f93aa2");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "68f6f7f8-274c-44e5-8ded-8933f6611a4d", "AQAAAAEAACcQAAAAEGmLTndiHB6cLmDIffPw9XrqIx61eSjkJMH2yTz/tX8EtNOfKS4cJQMZW3//Y8Dizw==", new DateTime(2022, 8, 29, 13, 51, 23, 271, DateTimeKind.Local).AddTicks(3444) });

            migrationBuilder.AddForeignKey(
                name: "FK_SlideShows_Products_ProductId",
                table: "SlideShows",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
