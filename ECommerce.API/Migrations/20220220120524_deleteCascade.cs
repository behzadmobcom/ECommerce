using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class deleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogKeyword_Blogs_BlogsId",
                table: "BlogKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogKeyword_Keywords_KeywordsId",
                table: "BlogKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogAuthors_BlogAuthorId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Blogs_BlogsId",
                table: "BlogTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Tags_TagsId",
                table: "BlogTag");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_ProductCategoriesId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Products_ProductsId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_KeywordProduct_Keywords_KeywordsId",
                table: "KeywordProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_KeywordProduct_Products_ProductsId",
                table: "KeywordProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Products_ProductId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductAttributeGroup_ProductAttributeGroups_AttributeGroupProductsId",
                table: "ProductProductAttributeGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductAttributeGroup_Products_ProductsId",
                table: "ProductProductAttributeGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_HolooCompanies_HolooCompanyId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_SupplierId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSellCounts_Products_ProductId",
                table: "ProductSellCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Products_ProductsId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tags_TagsId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUserRanks_Products_ProductId",
                table: "ProductUserRanks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUserRanks_Users_UserId",
                table: "ProductUserRanks");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetails_Products_ProductId",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaim_UserRoles_RoleId",
                schema: "Security",
                table: "RoleClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_SendInformation_Cities_CityId",
                table: "SendInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_SendInformation_States_StateId",
                table: "SendInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_SlideShows_Products_ProductId",
                table: "SlideShows");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_Users_UserId",
                schema: "Security",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogin_Users_UserId",
                schema: "Security",
                table: "UserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_UserRoles_RoleId",
                schema: "Security",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UserId",
                schema: "Security",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_Users_UserId",
                schema: "Security",
                table: "UserToken");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Products_ProductId",
                table: "WishLists");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Users_UserId",
                table: "WishLists");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "341d7ad9-a9ad-47ca-8e5e-bce11e26e39a");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cebcce0f-4189-4cea-9873-8a15f7bfe284");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "6323fe9e-7e66-4e86-8085-d90ec708427e");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "29c5aad2-3807-4eba-ab63-1eca77381254", "AQAAAAEAACcQAAAAEPRTu9GdUZuU1oWYZhRQecgYn6j7TaJ8cKYP/730wbHThDl3xU74C2XOFNbCb8xIrw==", new DateTime(2022, 2, 20, 15, 35, 22, 664, DateTimeKind.Local).AddTicks(8836) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegisterDate" },
                values: new object[] { "b0242a38-ee83-4b93-8002-7c39f3f0328e", "AQAAAAEAACcQAAAAEGnf27HlZ5L9PUtfqGtorHc+woBhHyWSxj/3nmpjgp3UYnMLuqddFZtdEatn26Clsg==", new DateTime(2022, 2, 20, 15, 35, 22, 675, DateTimeKind.Local).AddTicks(1174) });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogKeyword_Blogs_BlogsId",
                table: "BlogKeyword",
                column: "BlogsId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogKeyword_Keywords_KeywordsId",
                table: "BlogKeyword",
                column: "KeywordsId",
                principalTable: "Keywords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogAuthors_BlogAuthorId",
                table: "Blogs",
                column: "BlogAuthorId",
                principalTable: "BlogAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryId",
                table: "Blogs",
                column: "BlogCategoryId",
                principalTable: "BlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Blogs_BlogsId",
                table: "BlogTag",
                column: "BlogsId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Tags_TagsId",
                table: "BlogTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_ProductCategoriesId",
                table: "CategoryProduct",
                column: "ProductCategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Products_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordProduct_Keywords_KeywordsId",
                table: "KeywordProduct",
                column: "KeywordsId",
                principalTable: "Keywords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordProduct_Products_ProductsId",
                table: "KeywordProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Products_ProductId",
                table: "Prices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductAttributeGroup_ProductAttributeGroups_AttributeGroupProductsId",
                table: "ProductProductAttributeGroup",
                column: "AttributeGroupProductsId",
                principalTable: "ProductAttributeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductAttributeGroup_Products_ProductsId",
                table: "ProductProductAttributeGroup",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_HolooCompanies_HolooCompanyId",
                table: "Products",
                column: "HolooCompanyId",
                principalTable: "HolooCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_SupplierId",
                table: "Products",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSellCounts_Products_ProductId",
                table: "ProductSellCounts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Products_ProductsId",
                table: "ProductTag",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Tags_TagsId",
                table: "ProductTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserRanks_Products_ProductId",
                table: "ProductUserRanks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserRanks_Users_UserId",
                table: "ProductUserRanks",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetails_Products_ProductId",
                table: "PurchaseOrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaim_UserRoles_RoleId",
                schema: "Security",
                table: "RoleClaim",
                column: "RoleId",
                principalSchema: "Security",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SendInformation_Cities_CityId",
                table: "SendInformation",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SendInformation_States_StateId",
                table: "SendInformation",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SlideShows_Products_ProductId",
                table: "SlideShows",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_Users_UserId",
                schema: "Security",
                table: "UserClaim",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogin_Users_UserId",
                schema: "Security",
                table: "UserLogin",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_UserRoles_RoleId",
                schema: "Security",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "Security",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_UserId",
                schema: "Security",
                table: "UserRole",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_Users_UserId",
                schema: "Security",
                table: "UserToken",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Products_ProductId",
                table: "WishLists",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Users_UserId",
                table: "WishLists",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogKeyword_Blogs_BlogsId",
                table: "BlogKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogKeyword_Keywords_KeywordsId",
                table: "BlogKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogAuthors_BlogAuthorId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Blogs_BlogsId",
                table: "BlogTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Tags_TagsId",
                table: "BlogTag");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_ProductCategoriesId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Products_ProductsId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_KeywordProduct_Keywords_KeywordsId",
                table: "KeywordProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_KeywordProduct_Products_ProductsId",
                table: "KeywordProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Products_ProductId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductAttributeGroup_ProductAttributeGroups_AttributeGroupProductsId",
                table: "ProductProductAttributeGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductAttributeGroup_Products_ProductsId",
                table: "ProductProductAttributeGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_HolooCompanies_HolooCompanyId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_SupplierId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSellCounts_Products_ProductId",
                table: "ProductSellCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Products_ProductsId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tags_TagsId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUserRanks_Products_ProductId",
                table: "ProductUserRanks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUserRanks_Users_UserId",
                table: "ProductUserRanks");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetails_Products_ProductId",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaim_UserRoles_RoleId",
                schema: "Security",
                table: "RoleClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_SendInformation_Cities_CityId",
                table: "SendInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_SendInformation_States_StateId",
                table: "SendInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_SlideShows_Products_ProductId",
                table: "SlideShows");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_Users_UserId",
                schema: "Security",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogin_Users_UserId",
                schema: "Security",
                table: "UserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_UserRoles_RoleId",
                schema: "Security",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UserId",
                schema: "Security",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_Users_UserId",
                schema: "Security",
                table: "UserToken");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Products_ProductId",
                table: "WishLists");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Users_UserId",
                table: "WishLists");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BlogKeyword_Blogs_BlogsId",
                table: "BlogKeyword",
                column: "BlogsId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogKeyword_Keywords_KeywordsId",
                table: "BlogKeyword",
                column: "KeywordsId",
                principalTable: "Keywords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogAuthors_BlogAuthorId",
                table: "Blogs",
                column: "BlogAuthorId",
                principalTable: "BlogAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryId",
                table: "Blogs",
                column: "BlogCategoryId",
                principalTable: "BlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Blogs_BlogsId",
                table: "BlogTag",
                column: "BlogsId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Tags_TagsId",
                table: "BlogTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_ProductCategoriesId",
                table: "CategoryProduct",
                column: "ProductCategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Products_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordProduct_Keywords_KeywordsId",
                table: "KeywordProduct",
                column: "KeywordsId",
                principalTable: "Keywords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KeywordProduct_Products_ProductsId",
                table: "KeywordProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Products_ProductId",
                table: "Prices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductAttributeGroup_ProductAttributeGroups_AttributeGroupProductsId",
                table: "ProductProductAttributeGroup",
                column: "AttributeGroupProductsId",
                principalTable: "ProductAttributeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductAttributeGroup_Products_ProductsId",
                table: "ProductProductAttributeGroup",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_HolooCompanies_HolooCompanyId",
                table: "Products",
                column: "HolooCompanyId",
                principalTable: "HolooCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_SupplierId",
                table: "Products",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSellCounts_Products_ProductId",
                table: "ProductSellCounts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Products_ProductsId",
                table: "ProductTag",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Tags_TagsId",
                table: "ProductTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserRanks_Products_ProductId",
                table: "ProductUserRanks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserRanks_Users_UserId",
                table: "ProductUserRanks",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetails_Products_ProductId",
                table: "PurchaseOrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaim_UserRoles_RoleId",
                schema: "Security",
                table: "RoleClaim",
                column: "RoleId",
                principalSchema: "Security",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SendInformation_Cities_CityId",
                table: "SendInformation",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SendInformation_States_StateId",
                table: "SendInformation",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SlideShows_Products_ProductId",
                table: "SlideShows",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_Users_UserId",
                schema: "Security",
                table: "UserClaim",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogin_Users_UserId",
                schema: "Security",
                table: "UserLogin",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_UserRoles_RoleId",
                schema: "Security",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "Security",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_UserId",
                schema: "Security",
                table: "UserRole",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_Users_UserId",
                schema: "Security",
                table: "UserToken",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Products_ProductId",
                table: "WishLists",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Users_UserId",
                table: "WishLists",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
