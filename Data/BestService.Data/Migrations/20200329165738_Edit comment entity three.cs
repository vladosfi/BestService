using Microsoft.EntityFrameworkCore.Migrations;

namespace BestService.Data.Migrations
{
    public partial class Editcommententitythree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Rating",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte));
        }
    }
}
