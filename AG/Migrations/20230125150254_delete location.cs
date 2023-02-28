using Microsoft.EntityFrameworkCore.Migrations;

namespace AG.Migrations
{
    public partial class deletelocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "location");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Points",
                table: "location",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
