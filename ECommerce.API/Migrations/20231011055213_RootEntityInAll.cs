using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class RootEntityInAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Units",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Units",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Units",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Units",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Suppliers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Suppliers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Stores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Stores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "States",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "States",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "States",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "States",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SlideShows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "SlideShows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "SlideShows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "SlideShows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Sizes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Sizes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Sizes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Sizes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "ProductComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "ProductComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProductAttributes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProductAttributes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProductAttributeGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "ProductAttributeGroups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "ProductAttributeGroups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProductAttributeGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "PaymentMethods",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "PaymentMethods",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Keywords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Keywords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Currencies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Currencies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Currencies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Currencies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Cities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Cities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BlogCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "BlogCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "BlogCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "BlogCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BlogAuthors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "BlogAuthors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditorUserId",
                table: "BlogAuthors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "BlogAuthors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6809), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6810) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6812), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6813) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6814), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6814) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6815), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6816) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6817), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6817) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6818), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6819) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6819), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6820) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6821), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6821) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6822), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6823) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6823), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6824) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6826), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6827), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6827) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6828), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6829) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6830), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6830) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6831), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6831) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6832), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6833) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6834), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6834) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6835), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6836), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6837) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6838), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6838) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6839), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6839) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6840), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6841) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6841), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6842) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6843), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6843) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6872), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6873) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6874), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6875) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6876), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6876) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6877), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6877) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6878), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6879) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6880), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6880) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6881), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6881) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6882), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6883) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6883), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6884) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6885), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6885) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6886), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6887) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6888), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6889) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6890), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6890) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6891), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6891) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6892), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6893) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6894), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6894) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6895), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6895) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6896), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6897) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6898), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6898) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6899), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6899) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6900), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6901) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6901), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6902) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6903), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6903) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6904), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6905) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6905), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6906) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6907), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6907) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6908), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6908) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6909), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6910) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6911), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6911) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6912), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6912) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6913), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6915), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6915) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6916), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6916) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6917), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6918) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6919), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6919) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6920), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6920) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6922), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6922) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6923), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6924) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6925), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6925) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6926), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6926) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6927), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6928) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6929), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6929) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6930), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6930) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6931), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6932) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6933), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6933) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6934), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6934) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6935), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6936) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6937), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6937) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6938), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6938) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6939), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6940) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6941), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6941) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6942), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6942) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6943), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6944) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6945), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6945) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6946), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6946) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6947), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6948) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6949), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6949) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6950), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6951) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6952), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6952) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6953), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6953) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6954), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6955) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6957), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6957) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6958), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6959) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6959), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6960) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6961), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6961) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6962), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6963) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6963), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6964) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6965), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6965) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6966), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6967) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6968), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6968) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6969), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6969) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6970), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6971) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6972), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6972) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6973), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6973) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6974), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6975) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6976), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6976) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6977), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6978) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6979), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6979) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6980), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6981) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6982), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6982) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6983), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6983) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6984), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6985) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6986), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6986) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6987), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6988) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6988), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6989) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7015), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7016) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7017), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7017) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7018), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7019) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7020), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7020) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7021), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7021) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7022), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7023) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7024), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7024) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7025), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7025) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7026), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7027) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7028), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7028) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7029), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7029) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7030), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7031) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7031), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7032) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7033), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7033) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7034), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7034) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7035), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7036) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7037), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7037) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7038), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7038) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7039), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7040) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7041), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7041) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 130,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7042), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7042) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 131,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7043), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7044) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 132,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7045), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7045) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 133,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7046), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7046) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 134,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7047), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7048) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 135,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7050), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7050) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 136,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7051), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7052) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 137,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7053), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7053) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 138,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7054), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7054) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 139,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7055), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7056) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 140,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7056), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7057) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 141,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7058), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7058) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 142,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7059), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7059) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 143,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7060), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7061) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7062), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7062) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7063), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7063) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 146,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7064), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7065) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7066), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7066) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 148,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7067), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7067) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 149,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7068), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7069) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 150,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7070), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7070) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 151,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7071), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7071) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 152,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7072), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7073) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 153,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7073), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7074) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 154,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7075), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7075) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 155,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7076), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7077) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 156,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7078), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7078) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 157,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7079), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7079) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 158,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7080), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7081) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 159,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7081), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7082) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 160,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7084), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7084) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 161,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7085), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7085) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 162,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7086), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7087) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 163,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7088), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7088) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 164,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7089), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7089) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 165,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7090), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7091) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 166,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7092), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7092) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 167,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7093), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7093) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 168,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7094), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7095) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 169,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7095), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7096) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 170,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7097), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7097) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 171,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7098), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7099) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 172,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7099), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7100) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 173,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7101), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7101) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 174,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7102), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7102) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 175,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7103), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7104) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 176,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7105), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7105) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 177,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7106), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7106) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 178,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7107), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7108) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 179,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7109), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7109) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 180,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7110), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7110) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 181,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7111), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7112) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 182,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7113), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7113) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 183,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7114), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7114) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 184,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7118), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7119) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 185,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7120), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7120) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 186,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7121), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7121) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 187,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7122), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7123) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 188,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7124), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7124) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 189,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7125), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 190,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7126), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7127) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 191,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7128), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7128) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 192,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7129), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7129) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 193,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7130), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7131) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 194,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7131), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7132) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 195,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7156), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7157) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 196,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7158), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7158) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 197,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7159), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7159) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 198,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7160), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7161) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 199,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7162), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7162) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 200,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7163), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7163) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7164), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7165) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7166), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7166) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 203,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7167), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7167) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 204,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7168), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7169) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 205,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7170), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7170) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 206,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7171), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7171) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 207,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7172), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7173) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 208,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7174), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7174) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 209,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7176), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7176) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 210,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7177), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7178) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 211,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7179), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7179) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 212,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7180), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7180) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 213,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7181), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7182) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 214,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7182), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7183) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 215,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7184), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7184) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 216,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7185), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7186) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 217,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7186), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7187) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 218,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7188), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7188) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 219,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7189), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7190) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 220,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7190), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7191) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 221,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7192), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7192) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 222,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7193), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7194) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 223,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7194), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7195) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 224,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7196), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7197) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 225,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7198), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7198) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 226,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7199), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7199) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 227,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7200), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7201) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 228,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7201), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7202) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 229,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7203), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7203) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 230,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7204), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7205) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 231,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7205), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7206) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 232,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7207), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7207) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 233,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7208), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7209) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 234,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7210), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7211) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 235,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7212), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7212) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 236,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7213), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7213) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 237,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7214), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7215) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 238,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7216), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7216) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 239,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7218), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7218) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 240,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7219), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7220) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 241,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7220), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7221) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 242,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7222), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7222) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 243,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7223), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7224) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 244,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7224), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7225) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 245,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7226), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7226) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 246,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7227), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7228) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 247,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7228), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7229) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 248,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7230), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7230) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 249,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7231), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7232) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 250,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7232), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7233) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 251,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7234), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7235) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 252,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7235), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7236) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 253,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7237), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7237) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 254,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7238), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7239) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 255,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7240), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7240) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 256,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7241), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7241) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 257,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7242), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7243) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 258,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7244), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7245) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 259,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7246), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7246) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 260,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7247), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7248) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 261,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7248), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7249) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 262,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7250), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7250) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 263,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7251), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7252) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 264,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7252), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7253) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 265,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7254), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7254) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 266,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7255), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7256) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 267,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7256), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7257) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 268,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7258), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7258) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 269,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7259), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7260) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 270,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7260), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7261) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 271,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7262), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7262) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 272,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7263), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7264) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 273,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7264), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7265) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 274,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7266), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7266) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 275,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7267), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7268) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 276,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7268), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7269) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 277,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7270), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7270) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 278,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7271), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7272) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 279,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7272), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7273) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 280,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7297), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7297) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 281,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7298), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7299) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 282,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7300), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 283,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7302), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7303) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 284,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7303), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7304) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 285,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7305), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7305) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 286,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7306), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7307) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 287,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7307), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7308) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 288,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7309), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7309) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 289,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7310), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 290,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7311), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7312) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 291,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7313), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7313) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 292,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7314), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7314) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 293,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7315), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7316) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 294,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7317), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7317) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 295,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7318), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7318) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 296,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7319), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 297,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7321), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7321) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 298,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7322), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7322) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 299,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7323), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7324) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 300,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7325), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7325) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 301,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7326), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7326) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 302,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7327), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7328) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 303,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7329), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7329) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 304,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7330), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 305,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7331), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7332) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 306,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7333), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7333) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 307,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7334), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7334) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 308,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7336), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7337) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 309,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7337), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7338) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 310,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7339), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7339) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 311,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7340), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 312,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7341), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7342) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 313,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7343), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7343) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 314,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7344), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7344) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 315,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7345), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7346) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 316,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7347), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7347) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 317,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7348), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7348) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 318,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7349), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 319,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7351), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7351) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 320,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7352), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7352) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 321,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7354), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7354) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 322,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7355), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7356) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 323,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7356), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7357) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 324,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7358), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7358) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 325,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7359), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7360) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 326,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7361), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7361) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 327,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7362), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7362) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 328,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7363), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7364) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 329,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7365), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7365) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 330,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7366), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7366) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 331,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7367), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7368) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 332,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7369), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 333,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7371), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7371) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 334,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7372), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7373) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 335,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7373), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7374) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 336,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7375), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7375) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 337,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7376), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7377) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 338,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7377), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7378) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 339,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7379), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7379) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 340,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7380), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7381) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 341,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7381), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7382) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 342,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7383), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7383) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 343,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7384), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7384) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 344,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7385), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7386) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 345,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7387), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7387) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 346,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7388), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7389) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 347,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7389), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 348,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7391), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7391) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 349,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7392), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7393) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 350,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7393), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7394) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 351,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7395), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7395) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 352,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7396), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7396) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 353,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7397), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7398) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 354,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7399), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7399) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 355,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7400), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7400) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 356,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7401), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7402) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 357,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7403), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7404) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 358,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7405), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7405) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 359,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7406), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7407) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 360,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7407), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7408) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 361,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7409), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7409) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 362,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7410), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7411) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 363,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7411), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7412) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 364,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7413), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7413) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 365,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7457), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7458) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 366,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7459), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7459) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 367,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7460), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7461) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 368,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7461), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7462) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 369,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7463), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7463) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 370,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7464), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7465) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 371,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7465), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7466) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 372,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7467), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7467) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 373,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7468), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7469) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 374,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7469), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7470) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 375,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7471), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7471) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 376,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7472), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7473) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 377,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7473), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7474) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 378,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7475), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7475) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 379,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7476), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7476) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 380,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7477), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7478) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 381,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7479), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7479) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 382,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7481), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7481) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 383,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7482), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7483) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 384,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7484), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7484) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 385,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7485), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7485) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 386,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7486), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7487) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 387,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7488), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7488) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 388,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7489), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7489) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 389,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7490), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7491) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 390,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7492), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7492) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 391,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7493), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7493) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 392,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7494), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7495) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 393,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7496), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7496) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 394,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7497), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7497) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 395,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7498), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7499) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 396,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7499), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 397,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7501), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7501) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 398,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7502), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7502) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 399,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7503), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7504) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 400,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7508), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7508) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 401,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7509), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 402,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7511), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7512) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 403,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7513), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7513) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 404,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7514), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7514) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 405,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7515), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7516) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 406,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7516), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7517) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 407,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7518), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7518) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 408,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7519), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 409,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7520), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7521) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 410,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7522), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7522) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 411,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7523), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7524) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 412,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7524), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7525) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 413,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7526), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7526) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 414,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7527), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7527) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 415,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7528), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7529) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 416,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7530), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 417,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7531), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7531) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 418,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7532), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7533) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 419,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7534), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7534) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 420,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7535), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7536) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 421,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7536), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7537) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 422,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7538), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7538) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 423,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7539), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7539) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 424,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7540), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7541) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 425,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7542), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7542) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 426,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7543), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7543) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 427,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7544), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7545) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 428,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7546), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7546) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 429,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7547), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7547) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 430,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7548), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7549) });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 431,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7551), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7551) });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 244, DateTimeKind.Local).AddTicks(5354), new DateTime(2023, 10, 11, 9, 22, 12, 244, DateTimeKind.Local).AddTicks(5357) });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 244, DateTimeKind.Local).AddTicks(5416), new DateTime(2023, 10, 11, 9, 22, 12, 244, DateTimeKind.Local).AddTicks(5416) });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 244, DateTimeKind.Local).AddTicks(5437), new DateTime(2023, 10, 11, 9, 22, 12, 244, DateTimeKind.Local).AddTicks(5438) });

            migrationBuilder.UpdateData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 244, DateTimeKind.Local).AddTicks(5375), new DateTime(2023, 10, 11, 9, 22, 12, 244, DateTimeKind.Local).AddTicks(5375) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5438), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5451) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5456), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5456) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5457), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5458) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5459), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5459) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5460), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5461), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5462) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5463), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5463) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5464), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5464) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5465), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5466) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5466), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5467) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5468), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5468) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5469), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5469) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5500), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5501) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5502), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5502) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(5503), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6624) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6637), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6638) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6639), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6639) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6640), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6641) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6641), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6642) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6643), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6643) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6644), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6644) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6645), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6646) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6646), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6647) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6648), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6648) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6649), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6649) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6650), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6651) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6651), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6652) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6653), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6653) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6654), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6654) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6655), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6656) });

            migrationBuilder.UpdateData(
                table: "States",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6656), new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(6657) });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 244, DateTimeKind.Local).AddTicks(5452), new DateTime(2023, 10, 11, 9, 22, 12, 244, DateTimeKind.Local).AddTicks(5453) });

            migrationBuilder.UpdateData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 11, 9, 22, 12, 244, DateTimeKind.Local).AddTicks(5474), new DateTime(2023, 10, 11, 9, 22, 12, 244, DateTimeKind.Local).AddTicks(5474) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a3bfb494-d2b0-46d0-8e5f-8a625af349bf");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9a4569d0-553b-4ea2-b377-974daccda2da");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "884b40e3-8c44-4e58-9023-5d37768046b0");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "8c089081-c544-4bc6-aad7-b07aa8840588", "AQAAAAEAACcQAAAAEBnbLHgykAr1bjdiYYt382nlZNp8D1FmeVwLJDB0/r05hd95UuUhInNMwb9ArB6OyQ==", new DateTime(2023, 10, 11, 9, 22, 12, 242, DateTimeKind.Local).AddTicks(7775) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "States");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "States");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "States");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "States");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SlideShows");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "SlideShows");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "SlideShows");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SlideShows");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProductAttributeGroups");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "ProductAttributeGroups");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "ProductAttributeGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ProductAttributeGroups");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BlogAuthors");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "BlogAuthors");

            migrationBuilder.DropColumn(
                name: "EditorUserId",
                table: "BlogAuthors");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "BlogAuthors");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 10, 8, 12, 13, 33, 364, DateTimeKind.Local).AddTicks(628), new DateTime(2023, 10, 8, 12, 13, 33, 364, DateTimeKind.Local).AddTicks(628) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7ff8ea74-6727-46a0-942c-6fa51320ce9e");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e31f2147-1246-4eac-b1f9-12f15db094ba");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "140d6a45-0fb3-4a78-87fe-8ea1b1da0c5a");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "67edd8d6-ed34-4514-b657-6e70c47e8cd9", "AQAAAAEAACcQAAAAEE2mWKYiqXdpRpRpyWOImFHcnFfiAJel+V6Ld55FLgkelyEGa+084TTJLjbZ09MnuA==", new DateTime(2023, 10, 8, 12, 13, 33, 362, DateTimeKind.Local).AddTicks(8463) });
        }
    }
}
