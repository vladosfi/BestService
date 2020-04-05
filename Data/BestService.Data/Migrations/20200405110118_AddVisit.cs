using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BestService.Data.Migrations
{
    public partial class AddVisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_BaseCategories_BaseCategoryId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "BaseCategories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_BaseCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "BaseCategoryId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "VisitId",
                table: "Companies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Count = table.Column<long>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visits_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visits_CompanyId",
                table: "Visits",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_IsDeleted",
                table: "Visits",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_UserId",
                table: "Visits",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropColumn(
                name: "VisitId",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "BaseCategoryId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BaseCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BaseCategoryId",
                table: "Categories",
                column: "BaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseCategories_IsDeleted",
                table: "BaseCategories",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_BaseCategories_BaseCategoryId",
                table: "Categories",
                column: "BaseCategoryId",
                principalTable: "BaseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
