using Microsoft.EntityFrameworkCore.Migrations;

namespace BestService.Data.Migrations
{
    public partial class UpdateContactFormEnty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "ContactFormEntries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ip",
                table: "ContactFormEntries");
        }
    }
}
