using Microsoft.EntityFrameworkCore.Migrations;

namespace BestService.Data.Migrations
{
    public partial class EditVisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Visits_CompanyId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "VisitId",
                table: "Companies");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_CompanyId",
                table: "Visits",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Visits_CompanyId",
                table: "Visits");

            migrationBuilder.AddColumn<int>(
                name: "VisitId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_CompanyId",
                table: "Visits",
                column: "CompanyId",
                unique: true);
        }
    }
}
