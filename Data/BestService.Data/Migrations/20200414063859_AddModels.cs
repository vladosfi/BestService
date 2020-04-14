using Microsoft.EntityFrameworkCore.Migrations;

namespace BestService.Data.Migrations
{
    public partial class AddModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Companies_CompanyId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CompanyId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ParrentId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CompanyId",
                table: "Comments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParrentId",
                table: "Comments",
                column: "ParrentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Companies_CompanyId",
                table: "Comments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParrentId",
                table: "Comments",
                column: "ParrentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Companies_CompanyId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParrentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CompanyId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ParrentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ParrentId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CompanyId1",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CompanyId1",
                table: "Comments",
                column: "CompanyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Companies_CompanyId1",
                table: "Comments",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
