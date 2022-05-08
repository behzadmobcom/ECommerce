using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class ChangeNulls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "BlogAuthors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogAuthors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogCategories_BlogCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ColorCode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Percent = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAmount = table.Column<int>(type: "int", nullable: false),
                    MinOrder = table.Column<int>(type: "int", nullable: false),
                    MaxOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolooCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolooCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeywordText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrunchName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    PaymentMethodStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Tell = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConnectionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Like = table.Column<int>(type: "int", nullable: false),
                    Dislike = table.Column<int>(type: "int", nullable: false),
                    Visit = table.Column<int>(type: "int", nullable: false),
                    BlogAuthorId = table.Column<int>(type: "int", nullable: false),
                    BlogCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogAuthors_BlogAuthorId",
                        column: x => x.BlogAuthorId,
                        principalTable: "BlogAuthors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    DiscountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Few = table.Column<double>(type: "float", nullable: false),
                    UnitCode = table.Column<int>(type: "int", nullable: true),
                    assay = table.Column<double>(type: "float", nullable: true),
                    UnitWeight = table.Column<short>(type: "smallint", nullable: true),
                    HolooCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_HolooCompanies_HolooCompanyId",
                        column: x => x.HolooCompanyId,
                        principalTable: "HolooCompanies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AttributeType = table.Column<int>(type: "int", nullable: false),
                    AttributeGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_ProductAttributeGroups_AttributeGroupId",
                        column: x => x.AttributeGroupId,
                        principalTable: "ProductAttributeGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ArticleCode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    ArticleCodeCustomer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exist = table.Column<double>(type: "float", nullable: false),
                    MinOrder = table.Column<int>(type: "int", nullable: false),
                    MaxOrder = table.Column<int>(type: "int", nullable: true),
                    MinInStore = table.Column<double>(type: "float", nullable: true),
                    ReorderingLevel = table.Column<int>(type: "int", nullable: true),
                    IsDiscontinued = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    DiscountId = table.Column<int>(type: "int", nullable: true),
                    HolooCompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_HolooCompanies_HolooCompanyId",
                        column: x => x.HolooCompanyId,
                        principalTable: "HolooCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HireDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Commission = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    CustomerCodeCustomer = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    UserRoleId = table.Column<int>(type: "int", nullable: true),
                    HolooCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_HolooCompanies_HolooCompanyId",
                        column: x => x.HolooCompanyId,
                        principalTable: "HolooCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalSchema: "Security",
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogKeyword",
                columns: table => new
                {
                    BlogsId = table.Column<int>(type: "int", nullable: false),
                    KeywordsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogKeyword", x => new { x.BlogsId, x.KeywordsId });
                    table.ForeignKey(
                        name: "FK_BlogKeyword_Blogs_BlogsId",
                        column: x => x.BlogsId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogKeyword_Keywords_KeywordsId",
                        column: x => x.KeywordsId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogTag",
                columns: table => new
                {
                    BlogsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTag", x => new { x.BlogsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_BlogTag_Blogs_BlogsId",
                        column: x => x.BlogsId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsColleague = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmedColleague = table.Column<bool>(type: "bit", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    IsHaveCustomerCode = table.Column<bool>(type: "bit", nullable: false),
                    LicensePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsFeeder = table.Column<bool>(type: "bit", nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    CustomerCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    CustomerCodeCustomer = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    UserRoleId = table.Column<int>(type: "int", nullable: true),
                    HolooCompanyId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_HolooCompanies_HolooCompanyId",
                        column: x => x.HolooCompanyId,
                        principalTable: "HolooCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalSchema: "Security",
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    ProductCategoriesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.ProductCategoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_ProductCategoriesId",
                        column: x => x.ProductCategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Alt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    BlogId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KeywordProduct",
                columns: table => new
                {
                    KeywordsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordProduct", x => new { x.KeywordsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_KeywordProduct_Keywords_KeywordsId",
                        column: x => x.KeywordsId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeywordProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    MinQuantity = table.Column<int>(type: "int", nullable: false),
                    MaxQuantity = table.Column<int>(type: "int", nullable: false),
                    IsColleague = table.Column<bool>(type: "bit", nullable: false),
                    SellNumber = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    SizeId = table.Column<int>(type: "int", nullable: true),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prices_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prices_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prices_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductAttributeId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductProductAttributeGroup",
                columns: table => new
                {
                    AttributeGroupProductsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductAttributeGroup", x => new { x.AttributeGroupProductsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductProductAttributeGroup_ProductAttributeGroups_AttributeGroupProductsId",
                        column: x => x.AttributeGroupProductsId,
                        principalTable: "ProductAttributeGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductProductAttributeGroup_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSellCounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSellCounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSellCounts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTag",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTag", x => new { x.ProductsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ProductTag_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SlideShows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideShows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlideShows_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsAnswered = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AnswerId = table.Column<int>(type: "int", nullable: true),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogComments_BlogComments_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "BlogComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogComments_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogComments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogComments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsAnswered = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AnswerId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductComments_ProductComments_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "ProductComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductComments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductComments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductUserRanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUserRanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductUserRanks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductUserRanks_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SendInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SendInformation_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SendInformation_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SendInformation_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    RefId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    SanadCode = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    HolooCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_HolooCompanies_HolooCompanyId",
                        column: x => x.HolooCompanyId,
                        principalTable: "HolooCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                schema: "Security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishLists_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WishLists_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    ExpectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    DiscountAmount = table.Column<int>(type: "int", nullable: true),
                    ShippingFee = table.Column<int>(type: "int", nullable: true),
                    Taxes = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountantDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    FBailCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    ShippingId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    SubmittedById = table.Column<int>(type: "int", nullable: true),
                    AccountantId = table.Column<int>(type: "int", nullable: true),
                    DiscountId = table.Column<int>(type: "int", nullable: true),
                    TransactionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Employees_AccountantId",
                        column: x => x.AccountantId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Employees_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Employees_SubmittedById",
                        column: x => x.SubmittedById,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Shipping_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shipping",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    SumPrice = table.Column<int>(type: "int", nullable: false),
                    DiscountAmount = table.Column<int>(type: "int", nullable: true),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "ColorCode", "Name" },
                values: new object[] { 1, "#FFFFFF", "بدون رنگ بندی" });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Amount", "Name" },
                values: new object[,]
                {
                    { 1, 1, "تومان" },
                    { 2, 24000, "دلار" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "Amount", "Code", "EndDate", "IsActive", "MaxAmount", "MaxOrder", "MinOrder", "Name", "Percent", "StartDate" },
                values: new object[] { 1, 0, "NoDiscount", null, false, 0, 0, 0, "بدون تخفیف", 0.0, null });

            migrationBuilder.InsertData(
                table: "HolooCompanies",
                columns: new[] { "Id", "ConnectionString", "Name" },
                values: new object[] { 1, "Server=.\\mssql2008r2;Database=Holoo1;Trusted_Connection=True;MultipleActiveResultSets=true;", "Holoo1" });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 1, "Currency", "تومان" });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, null, "بدون سایز بندی" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "آذربايجان شرقي" },
                    { 2, "آذربايجان غربي" },
                    { 3, "اردبيل" },
                    { 4, "اصفهان" },
                    { 5, "البرز" },
                    { 6, "ايلام" },
                    { 7, "بوشهر" },
                    { 8, "تهران" },
                    { 9, "چهارمحال و بختياري" },
                    { 10, "خراسان جنوبي" },
                    { 11, "خراسان رضوي" },
                    { 12, "خراسان شمالي" },
                    { 13, "خوزستان" },
                    { 14, "زنجان" },
                    { 15, "سمنان" },
                    { 16, "سيستان و بلوچستان" },
                    { 17, "فارس" },
                    { 18, "قزوين" },
                    { 19, "قم" },
                    { 20, "کردستان" },
                    { 21, "کرمان" },
                    { 22, "کرمانشاه" },
                    { 23, "کهکيلويه و بويراحمد" },
                    { 24, "گلستان" },
                    { 25, "گيلان" },
                    { 26, "لرستان" },
                    { 27, "مازندران" },
                    { 28, "مرکزي" },
                    { 29, "هرمزگان" },
                    { 30, "همدان" },
                    { 31, "يزد" }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { 1, null, "انبار پیش فرض" });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "ConnectionName", "Description", "Email", "Mobile", "Name", "Tell", "Website" },
                values: new object[] { 1, null, null, null, null, null, "تامین کننده پیش فرض", null, null });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "14c87bba-96d8-4de2-86b9-8061925a3ade", "SuperAdmin", "SUPERADMIN" },
                    { 2, "cb6c3deb-cf57-4331-87a0-6e5a29c006da", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 4, "3b1ff526-876a-4c7c-972e-fdcf5b2c53b2", "Client", "Client" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 1, "اهر", 1 },
                    { 2, "عجبشير", 1 },
                    { 3, "آذر شهر", 1 },
                    { 4, "بناب", 1 },
                    { 5, "بستان آباد", 1 },
                    { 6, "چاراويماق", 1 },
                    { 7, "هشترود", 1 },
                    { 8, "هريس", 1 },
                    { 9, "جلفا", 1 },
                    { 10, "كليبر", 1 },
                    { 11, "خداآفرين", 1 },
                    { 12, "ملكان", 1 },
                    { 13, "مراغه", 1 },
                    { 14, "ميانه", 1 },
                    { 15, "مرند", 1 },
                    { 16, "اسكو", 1 },
                    { 17, "سراب", 1 },
                    { 18, "شبستر", 1 },
                    { 19, "تبريز", 1 },
                    { 20, "ورزقان", 1 },
                    { 21, "اروميه", 2 },
                    { 22, "نقده", 2 },
                    { 23, "ماكو", 2 },
                    { 24, "تكاب", 2 },
                    { 25, "خوي", 2 },
                    { 26, "مهاباد", 2 },
                    { 27, "سر دشت", 2 },
                    { 28, "چالدران", 2 },
                    { 29, "بوكان", 2 },
                    { 30, "مياندوآب", 2 },
                    { 31, "سلماس", 2 },
                    { 32, "شاهين دژ", 2 },
                    { 33, "پيرانشهر", 2 },
                    { 34, "اشنويه", 2 },
                    { 35, "چايپاره", 2 },
                    { 36, "پلدشت", 2 },
                    { 37, "شوط", 2 },
                    { 38, "اردبيل", 3 },
                    { 39, "سرعين", 3 },
                    { 40, "بيله سوار", 3 },
                    { 41, "پارس آباد", 3 },
                    { 42, "خلخال", 3 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 43, "مشگين شهر", 3 },
                    { 44, "نمين", 3 },
                    { 45, "نير", 3 },
                    { 46, "كوثر", 3 },
                    { 47, "گرمي", 3 },
                    { 48, "بوئين و مياندشت", 4 },
                    { 49, "مباركه", 4 },
                    { 50, "اردستان", 4 },
                    { 51, "خور و بيابانک", 4 },
                    { 52, "فلاورجان", 4 },
                    { 53, "فريدون شهر", 4 },
                    { 54, "كاشان", 4 },
                    { 55, "لنجان", 4 },
                    { 56, "گلپايگان", 4 },
                    { 57, "فريدن", 4 },
                    { 58, "نايين", 4 },
                    { 59, "اصفهان", 4 },
                    { 60, "نجف آباد", 4 },
                    { 61, "آران و بيدگل", 4 },
                    { 62, "چادگان", 4 },
                    { 63, "تيران و کرون", 4 },
                    { 64, "شهرضا", 4 },
                    { 65, "سميرم", 4 },
                    { 66, "خميني شهر", 4 },
                    { 67, "دهاقان", 4 },
                    { 68, "نطنز", 4 },
                    { 69, "برخوار", 4 },
                    { 70, "شاهين شهر و ميمه", 4 },
                    { 71, "خوانسار", 4 },
                    { 72, "ايلام", 6 },
                    { 73, "مهران", 6 },
                    { 74, "دهلران", 6 },
                    { 75, "آبدانان", 6 },
                    { 76, "چرداول", 6 },
                    { 77, "دره شهر", 6 },
                    { 78, "ايوان", 6 },
                    { 79, "بدره", 6 },
                    { 80, "سيروان", 6 },
                    { 81, "ملکشاهي", 6 },
                    { 82, "عسلويه", 7 },
                    { 83, "گناوه", 7 },
                    { 84, "دشتي", 7 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 85, "دشتستان", 7 },
                    { 86, "دير", 7 },
                    { 87, "بوشهر", 7 },
                    { 88, "كنگان", 7 },
                    { 89, "تنگستان", 7 },
                    { 90, "ديلم", 7 },
                    { 91, "جم", 7 },
                    { 92, "قرچك", 8 },
                    { 93, "پرديس", 8 },
                    { 94, "بهارستان", 8 },
                    { 95, "شميرانات", 8 },
                    { 96, "رباط كريم", 8 },
                    { 97, "فيروزكوه", 8 },
                    { 98, "تهران", 8 },
                    { 99, "ورامين", 8 },
                    { 100, "اسلامشهر", 8 },
                    { 101, "ري", 8 },
                    { 102, "پاكدشت", 8 },
                    { 103, "پيشوا", 8 },
                    { 104, "قدس", 8 },
                    { 105, "ملارد", 8 },
                    { 106, "شهريار", 8 },
                    { 107, "دماوند", 8 },
                    { 108, "بن", 9 },
                    { 109, "سامان", 9 },
                    { 110, "کيار", 9 },
                    { 111, "بروجن", 9 },
                    { 112, "اردل", 9 },
                    { 113, "شهركرد", 9 },
                    { 114, "فارسان", 9 },
                    { 115, "کوهرنگ", 9 },
                    { 116, "لردگان", 9 },
                    { 117, "داورزن", 11 },
                    { 118, "كلات", 11 },
                    { 119, "بردسكن", 11 },
                    { 120, "مشهد", 11 },
                    { 121, "نيشابور", 11 },
                    { 122, "سبزوار", 11 },
                    { 123, "كاشمر", 11 },
                    { 124, "گناباد", 11 },
                    { 125, "تربت حيدريه", 11 },
                    { 126, "خواف", 11 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 127, "تربت جام", 11 },
                    { 128, "تايباد", 11 },
                    { 129, "مه ولات", 11 },
                    { 130, "چناران", 11 },
                    { 131, "درگز", 11 },
                    { 132, "فيروزه", 11 },
                    { 133, "قوچان", 11 },
                    { 134, "سرخس", 11 },
                    { 135, "رشتخوار", 11 },
                    { 136, "بينالود", 11 },
                    { 137, "زاوه", 11 },
                    { 138, "جوين", 11 },
                    { 139, "بجستان", 11 },
                    { 140, "باخزر", 11 },
                    { 141, "فريمان", 11 },
                    { 142, "خليل آباد", 11 },
                    { 143, "جغتاي", 11 },
                    { 144, "خوشاب", 11 },
                    { 145, "زيرکوه", 10 },
                    { 146, "خوسف", 10 },
                    { 147, "درميان", 10 },
                    { 148, "قائنات", 10 },
                    { 149, "بشرويه", 10 },
                    { 150, "فردوس", 10 },
                    { 151, "بيرجند", 10 },
                    { 152, "نهبندان", 10 },
                    { 153, "سربيشه", 10 },
                    { 154, "سرايان", 10 },
                    { 155, "طبس", 11 },
                    { 156, "بجنورد", 12 },
                    { 157, "راز و جرگلان", 12 },
                    { 158, "اسفراين", 12 },
                    { 159, "جاجرم", 12 },
                    { 160, "شيروان", 12 },
                    { 161, "مانه و سملقان", 12 },
                    { 162, "گرمه", 12 },
                    { 163, "فاروج", 12 },
                    { 164, "کارون", 13 },
                    { 165, "حميديه", 13 },
                    { 166, "آغاجري", 13 },
                    { 167, "شوشتر", 13 },
                    { 168, "دشت آزادگان", 13 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 169, "اميديه", 13 },
                    { 170, "گتوند", 13 },
                    { 171, "شادگان", 13 },
                    { 172, "دزفول", 13 },
                    { 173, "رامشير", 13 },
                    { 174, "بهبهان", 13 },
                    { 175, "باوي", 13 },
                    { 176, "انديمشك", 13 },
                    { 177, "اهواز", 13 },
                    { 178, "انديکا", 13 },
                    { 179, "شوش", 13 },
                    { 180, "آبادان", 13 },
                    { 181, "هنديجان", 13 },
                    { 182, "خرمشهر", 13 },
                    { 183, "مسجد سليمان", 13 },
                    { 184, "ايذه", 13 },
                    { 185, "رامهرمز", 13 },
                    { 186, "باغ ملك", 13 },
                    { 187, "هفتکل", 13 },
                    { 188, "هويزه", 13 },
                    { 189, "ماهشهر", 13 },
                    { 190, "لالي", 13 },
                    { 191, "زنجان", 14 },
                    { 192, "ابهر", 14 },
                    { 193, "خدابنده", 14 },
                    { 194, "ماهنشان", 14 },
                    { 195, "خرمدره", 14 },
                    { 196, "ايجرود", 14 },
                    { 197, "طارم", 14 },
                    { 198, "سلطانيه", 14 },
                    { 199, "سمنان", 15 },
                    { 200, "شاهرود", 15 },
                    { 201, "گرمسار", 15 },
                    { 202, "سرخه", 15 },
                    { 203, "دامغان", 15 },
                    { 204, "آرادان", 15 },
                    { 205, "مهدي شهر", 15 },
                    { 206, "ميامي", 15 },
                    { 207, "زاهدان", 16 },
                    { 208, "بمپور", 16 },
                    { 209, "چابهار", 16 },
                    { 210, "خاش", 16 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 211, "سراوان", 16 },
                    { 212, "زابل", 16 },
                    { 213, "سرباز", 16 },
                    { 214, "قصر قند", 16 },
                    { 215, "نيكشهر", 16 },
                    { 216, "کنارک", 16 },
                    { 217, "ايرانشهر", 16 },
                    { 218, "زهک", 16 },
                    { 219, "سيب و سوران", 16 },
                    { 220, "ميرجاوه", 16 },
                    { 221, "دلگان", 16 },
                    { 222, "هيرمند", 16 },
                    { 223, "مهرستان", 16 },
                    { 224, "فنوج", 16 },
                    { 225, "هامون", 16 },
                    { 226, "نيمروز", 16 },
                    { 227, "شيراز", 17 },
                    { 228, "اقليد", 17 },
                    { 229, "داراب", 17 },
                    { 230, "فسا	", 17 },
                    { 231, "مرودشت", 17 },
                    { 232, "خرم بيد", 17 },
                    { 233, "آباده", 17 },
                    { 234, "كازرون", 17 },
                    { 235, "گراش", 17 },
                    { 236, "ممسني", 17 },
                    { 237, "سپيدان", 17 },
                    { 238, "لارستان", 17 },
                    { 239, "فيروز آباد", 17 },
                    { 240, "جهرم", 17 },
                    { 241, "ني ريز", 17 },
                    { 242, "استهبان", 17 },
                    { 243, "لامرد", 17 },
                    { 244, "مهر", 17 },
                    { 245, "پاسارگاد", 17 },
                    { 246, "ارسنجان", 17 },
                    { 247, "قيروكارزين", 17 },
                    { 248, "رستم", 17 },
                    { 249, "فراشبند", 17 },
                    { 250, "سروستان", 17 },
                    { 251, "زرين دشت", 17 },
                    { 252, "کوار", 17 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 253, "بوانات", 17 },
                    { 254, "خرامه", 17 },
                    { 255, "خنج", 17 },
                    { 256, "قزوين", 18 },
                    { 257, "تاكستان", 18 },
                    { 258, "آبيك", 18 },
                    { 259, "بوئين زهرا", 18 },
                    { 260, "البرز", 18 },
                    { 261, "آوج", 18 },
                    { 262, "قم", 19 },
                    { 263, "طالقان", 5 },
                    { 264, "اشتهارد", 5 },
                    { 265, "كرج", 5 },
                    { 266, "نظر آباد", 5 },
                    { 267, "ساوجبلاغ‎", 5 },
                    { 268, "فرديس", 5 },
                    { 269, "سنندج", 20 },
                    { 270, "ديواندره", 20 },
                    { 271, "بانه", 20 },
                    { 272, "بيجار", 20 },
                    { 273, "سقز", 20 },
                    { 274, "كامياران", 20 },
                    { 275, "قروه", 20 },
                    { 276, "مريوان", 20 },
                    { 277, "سروآباد", 20 },
                    { 278, "دهگلان‎", 20 },
                    { 279, "كرمان", 21 },
                    { 280, "راور", 21 },
                    { 281, "شهر بابک", 21 },
                    { 282, "انار", 21 },
                    { 283, "کوهبنان", 21 },
                    { 284, "رفسنجان", 21 },
                    { 285, "سيرجان", 21 },
                    { 286, "كهنوج", 21 },
                    { 287, "زرند", 21 },
                    { 288, "ريگان", 21 },
                    { 289, "بم", 21 },
                    { 290, "جيرفت", 21 },
                    { 291, "عنبرآباد", 21 },
                    { 292, "بافت", 21 },
                    { 293, "ارزوئيه", 21 },
                    { 294, "بردسير", 21 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 295, "فهرج", 21 },
                    { 296, "فارياب", 21 },
                    { 297, "منوجان", 21 },
                    { 298, "نرماشير", 21 },
                    { 299, "قلعه گنج", 21 },
                    { 300, "رابر", 21 },
                    { 301, "رودبار جنوب", 21 },
                    { 302, "كرمانشاه", 22 },
                    { 303, "اسلام آباد غرب", 22 },
                    { 304, "سر پل ذهاب", 22 },
                    { 305, "كنگاور", 22 },
                    { 306, "سنقر", 22 },
                    { 307, "قصر شيرين", 22 },
                    { 308, "گيلان غرب", 22 },
                    { 309, "هرسين", 22 },
                    { 310, "صحنه", 22 },
                    { 311, "پاوه", 22 },
                    { 312, "جوانرود", 22 },
                    { 313, "دالاهو", 22 },
                    { 314, "روانسر", 22 },
                    { 315, "ثلاث باباجاني", 22 },
                    { 316, "ياسوج", 23 },
                    { 317, "گچساران", 23 },
                    { 318, "دنا", 23 },
                    { 319, "کهگيلويه‎", 23 },
                    { 320, "لنده", 23 },
                    { 321, "بهمئي", 23 },
                    { 322, "باشت", 23 },
                    { 323, "بويراحمد", 23 },
                    { 324, "چرام", 23 },
                    { 325, "گرگان", 24 },
                    { 326, "آق قلا", 24 },
                    { 327, "گنبد كاووس", 24 },
                    { 328, "علي آباد", 24 },
                    { 329, "مينو دشت", 24 },
                    { 330, "تركمن", 24 },
                    { 331, "كردكوي", 24 },
                    { 332, "بندر گز", 24 },
                    { 333, "كلاله", 24 },
                    { 334, "آزاد شهر", 24 },
                    { 335, "راميان", 24 },
                    { 336, "گاليکش‎", 24 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 337, "مراوه تپه", 24 },
                    { 338, "گميشان", 24 },
                    { 339, "رشت", 25 },
                    { 340, "لنگرود", 25 },
                    { 341, "رودسر", 25 },
                    { 342, "طوالش", 25 },
                    { 343, "آستارا", 25 },
                    { 344, "آستانه اشرفيه", 25 },
                    { 345, "رودبار", 25 },
                    { 346, "فومن", 25 },
                    { 347, "صومعه سرا", 25 },
                    { 348, "بندرانزلي", 25 },
                    { 349, "رضوانشهر", 25 },
                    { 350, "ماسال", 25 },
                    { 351, "شفت", 25 },
                    { 352, "سياهكل", 25 },
                    { 353, "املش", 25 },
                    { 354, "لاهيجان", 25 },
                    { 355, "خرم آباد", 26 },
                    { 356, "دلفان", 26 },
                    { 357, "بروجرد", 26 },
                    { 358, "دورود", 26 },
                    { 359, "اليگودرز", 26 },
                    { 360, "ازنا", 26 },
                    { 361, "كوهدشت", 26 },
                    { 362, "سلسله", 26 },
                    { 363, "پلدختر", 26 },
                    { 364, "دوره", 26 },
                    { 365, "رومشکان", 26 },
                    { 366, "ساري", 27 },
                    { 367, "آمل", 27 },
                    { 368, "بابل", 27 },
                    { 369, "بابلسر", 27 },
                    { 370, "بهشهر", 27 },
                    { 371, "تنكابن", 27 },
                    { 372, "جويبار", 27 },
                    { 373, "چالوس", 27 },
                    { 374, "رامسر", 27 },
                    { 375, "سواد كوه", 27 },
                    { 376, "قائم شهر", 27 },
                    { 377, "نكا", 27 },
                    { 378, "نور", 27 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 379, "نوشهر", 27 },
                    { 380, "محمودآباد", 27 },
                    { 381, "فريدونکنار", 27 },
                    { 382, "عباس آباد", 27 },
                    { 383, "گلوگاه", 27 },
                    { 384, "مياندورود", 27 },
                    { 385, "سيمرغ", 27 },
                    { 386, "کلاردشت", 27 },
                    { 387, "سوادکوه شمالي", 27 },
                    { 388, "اراك", 28 },
                    { 389, "آشتيان", 28 },
                    { 390, "تفرش", 28 },
                    { 391, "خمين", 28 },
                    { 392, "دليجان", 28 },
                    { 393, "ساوه", 28 },
                    { 394, "زرنديه", 28 },
                    { 395, "محلات", 28 },
                    { 396, "شازند", 28 },
                    { 397, "فراهان", 28 },
                    { 398, "خنداب", 28 },
                    { 399, "کميجان", 28 },
                    { 400, "بندرعباس", 29 },
                    { 401, "قشم", 29 },
                    { 402, "بندر لنگه", 29 },
                    { 403, "بستك", 29 },
                    { 404, "حاجي آباد هرمزگان", 29 },
                    { 405, "رودان", 29 },
                    { 406, "ميناب", 29 },
                    { 407, "ابوموسي", 29 },
                    { 408, "جاسک", 29 },
                    { 409, "خمير", 29 },
                    { 410, "پارسيان", 29 },
                    { 411, "بشاگرد", 29 },
                    { 412, "سيريک", 29 },
                    { 413, "حاجي آباد", 29 },
                    { 414, "همدان", 30 },
                    { 415, "ملاير", 30 },
                    { 416, "تويسركان", 30 },
                    { 417, "نهاوند", 30 },
                    { 418, "كبودر اهنگ", 30 },
                    { 419, "رزن", 30 },
                    { 420, "اسدآباد", 30 }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 421, "بهار", 30 },
                    { 422, "فامنين", 30 },
                    { 423, "يزد", 31 },
                    { 424, "تفت", 31 },
                    { 425, "اردكان", 31 },
                    { 426, "ابركوه", 31 },
                    { 427, "ميبد", 31 },
                    { 428, "بافق", 31 },
                    { 429, "صدوق", 31 },
                    { 430, "مهريز", 31 },
                    { 431, "خاتم", 31 }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "CityId", "CompanyName", "ConcurrencyStamp", "CustomerCode", "CustomerCodeCustomer", "Email", "EmailConfirmed", "FatherName", "FirstName", "HolooCompanyId", "IsActive", "IsColleague", "IsConfirmedColleague", "IsFeeder", "IsHaveCustomerCode", "LastName", "LicensePath", "LockoutEnabled", "LockoutEnd", "Mobile", "NationalCode", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PicturePath", "RegisterDate", "SecurityStamp", "StateId", "TwoFactorEnabled", "UserName", "UserRoleId" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "488b3fc6-c41e-4a51-92eb-fd3cf272f844", null, null, "sayyah.alireza@gmail.com", true, null, "Alireza", 1, true, false, false, false, false, "Sayyah", null, false, null, "No Mobile", null, "SAYYAH.ALIREZA@GMAIL.COM", "SUPERADMIN", "AQAAAAEAACcQAAAAEMPCN8+HW0kYeNRm7KuURaYYk676fiJ2ZeYxMM38W2xND/1plAoLxk1oW+MRNjeZpA==", "0911307006", true, null, new DateTime(2022, 2, 6, 9, 2, 35, 9, DateTimeKind.Local).AddTicks(8977), "", null, false, "superadmin", 1 },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "57dc13a0-e9a5-4a4b-a526-c35f31de0a97", null, null, "sajjad.nazmi@gmail.com", true, null, "Sajjad", 1, true, false, false, false, false, "Nazmi", null, false, null, "No Mobile", null, "SAJJAD.NAZMI@GMAIL.COM", "SAJJADNAZMI", "AQAAAAEAACcQAAAAEO/MVtvpAPe23f7Mu2qjKmL7jQXZhy3BYR0Os9KHkYIXDGAXnWAbipYiGMhCALWIsw==", "09119394726", true, null, new DateTime(2022, 2, 6, 9, 2, 35, 20, DateTimeKind.Local).AddTicks(5815), "", null, false, "sajjadnazmi", 1 }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_ParentId",
                table: "BlogCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_AnswerId",
                table: "BlogComments",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_BlogId",
                table: "BlogComments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_EmployeeId",
                table: "BlogComments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_UserId",
                table: "BlogComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogKeyword_KeywordsId",
                table: "BlogKeyword",
                column: "KeywordsId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogAuthorId",
                table: "Blogs",
                column: "BlogAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogCategoryId",
                table: "Blogs",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTag_TagsId",
                table: "BlogTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DiscountId",
                table: "Categories",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_HolooCompanyId",
                table: "Employees",
                column: "HolooCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserRoleId",
                table: "Employees",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_BlogId",
                table: "Images",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordProduct_ProductsId",
                table: "KeywordProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ColorId",
                table: "Prices",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_CurrencyId",
                table: "Prices",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ProductId",
                table: "Prices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_SizeId",
                table: "Prices",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_UnitId",
                table: "Prices",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeGroupId",
                table: "ProductAttributes",
                column: "AttributeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductAttributeId",
                table: "ProductAttributeValues",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductId",
                table: "ProductAttributeValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_AnswerId",
                table: "ProductComments",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_EmployeeId",
                table: "ProductComments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductId",
                table: "ProductComments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttributeGroup_ProductsId",
                table: "ProductProductAttributeGroup",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DiscountId",
                table: "Products",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_HolooCompanyId",
                table: "Products",
                column: "HolooCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreId",
                table: "Products",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSellCounts_ProductId",
                table: "ProductSellCounts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_TagsId",
                table: "ProductTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserRanks_ProductId",
                table: "ProductUserRanks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserRanks_UserId",
                table: "ProductUserRanks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_DiscountId",
                table: "PurchaseOrderDetails",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_ProductId",
                table: "PurchaseOrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_PurchaseOrderId",
                table: "PurchaseOrderDetails",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_AccountantId",
                table: "PurchaseOrders",
                column: "AccountantId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ApprovedById",
                table: "PurchaseOrders",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_DiscountId",
                table: "PurchaseOrders",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ShippingId",
                table: "PurchaseOrders",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SubmittedById",
                table: "PurchaseOrders",
                column: "SubmittedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_TransactionId",
                table: "PurchaseOrders",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_UserId",
                table: "PurchaseOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                schema: "Security",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SendInformation_CityId",
                table: "SendInformation",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SendInformation_StateId",
                table: "SendInformation",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_SendInformation_UserId",
                table: "SendInformation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SlideShows_ProductId",
                table: "SlideShows",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_HolooCompanyId",
                table: "Transactions",
                column: "HolooCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentMethodId",
                table: "Transactions",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_HolooCompanyId",
                table: "Units",
                column: "HolooCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                schema: "Security",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                schema: "Security",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "Security",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Security",
                table: "UserRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Security",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                schema: "Security",
                table: "Users",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HolooCompanyId",
                schema: "Security",
                table: "Users",
                column: "HolooCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StateId",
                schema: "Security",
                table: "Users",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                schema: "Security",
                table: "Users",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Security",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_ProductId",
                table: "WishLists",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_UserId",
                table: "WishLists",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogComments");

            migrationBuilder.DropTable(
                name: "BlogKeyword");

            migrationBuilder.DropTable(
                name: "BlogTag");

            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "KeywordProduct");

            migrationBuilder.DropTable(
                name: "LoginHistories");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "ProductAttributeValues");

            migrationBuilder.DropTable(
                name: "ProductComments");

            migrationBuilder.DropTable(
                name: "ProductProductAttributeGroup");

            migrationBuilder.DropTable(
                name: "ProductSellCounts");

            migrationBuilder.DropTable(
                name: "ProductTag");

            migrationBuilder.DropTable(
                name: "ProductUserRanks");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "RoleClaim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "SendInformation");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "SlideShows");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserLogin",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "WishLists");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "BlogAuthors");

            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.DropTable(
                name: "ProductAttributeGroups");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "HolooCompanies");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
