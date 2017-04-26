using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LibraryManagement.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                    table.UniqueConstraint("AK_Author_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.UniqueConstraint("AK_Category_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                    table.UniqueConstraint("AK_Language_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Parameter",
                columns: table => new
                {
                    ParameterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParameterName = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.ParameterId);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                    table.UniqueConstraint("AK_Publisher_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "BookInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(type: "varchar(50)", nullable: true),
                    DateofImport = table.Column<DateTime>(nullable: false, defaultValueSql: "convert(datetime, getdate())"),
                    Description = table.Column<string>(nullable: true),
                    ISBN = table.Column<string>(type: "char(13)", maxLength: 13, nullable: false),
                    LanguageId = table.Column<int>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    PublicationDate = table.Column<DateTime>(nullable: false),
                    PublisherId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(type: "varchar(200)", nullable: false),
                    TotalBorrowed = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInfo", x => x.Id);
                    table.UniqueConstraint("AK_BookInfo_ISBN", x => x.ISBN);
                    table.ForeignKey(
                        name: "FK_BookInfo_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookInfo_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthorJoiner",
                columns: table => new
                {
                    ISBN = table.Column<string>(nullable: false),
                    AuthorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthorJoiner", x => new { x.ISBN, x.AuthorID });
                    table.ForeignKey(
                        name: "FK_BookAuthorJoiner_Author_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthorJoiner_BookInfo_ISBN",
                        column: x => x.ISBN,
                        principalTable: "BookInfo",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCategoryJoiner",
                columns: table => new
                {
                    ISBN = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategoryJoiner", x => new { x.ISBN, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BookCategoryJoiner_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCategoryJoiner_BookInfo_ISBN",
                        column: x => x.ISBN,
                        principalTable: "BookInfo",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCopyDetail",
                columns: table => new
                {
                    ISBN = table.Column<string>(nullable: false),
                    CopyNo = table.Column<int>(nullable: false),
                    Condition = table.Column<string>(nullable: true),
                    DateofImport = table.Column<DateTime>(nullable: false, defaultValueSql: "convert(datetime, getdate())"),
                    State = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopyDetail", x => new { x.ISBN, x.CopyNo });
                    table.ForeignKey(
                        name: "FK_BookCopyDetail_BookInfo_ISBN",
                        column: x => x.ISBN,
                        principalTable: "BookInfo",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthorJoiner_AuthorID",
                table: "BookAuthorJoiner",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategoryJoiner_CategoryId",
                table: "BookCategoryJoiner",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInfo_LanguageId",
                table: "BookInfo",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInfo_PublisherId",
                table: "BookInfo",
                column: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthorJoiner");

            migrationBuilder.DropTable(
                name: "BookCategoryJoiner");

            migrationBuilder.DropTable(
                name: "BookCopyDetail");

            migrationBuilder.DropTable(
                name: "Parameter");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "BookInfo");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
