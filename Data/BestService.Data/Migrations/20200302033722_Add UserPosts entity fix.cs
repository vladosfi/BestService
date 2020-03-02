using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BestService.Data.Migrations
{
    public partial class AddUserPostsentityfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPostId",
                table: "Companies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsersPosts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    PostText = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersPosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserPostId",
                table: "Companies",
                column: "UserPostId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPosts_IsDeleted",
                table: "UsersPosts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPosts_UserId",
                table: "UsersPosts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_UsersPosts_UserPostId",
                table: "Companies",
                column: "UserPostId",
                principalTable: "UsersPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_UsersPosts_UserPostId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "UsersPosts");

            migrationBuilder.DropIndex(
                name: "IX_Companies_UserPostId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UserPostId",
                table: "Companies");
        }
    }
}
