using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BestService.Data.Migrations
{
    public partial class AddSubscribe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Ratings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Ratings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Subscribes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Ip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_IsDeleted",
                table: "Ratings",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_IsDeleted",
                table: "Subscribes",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_IsDeleted",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Ratings");
        }
    }
}
