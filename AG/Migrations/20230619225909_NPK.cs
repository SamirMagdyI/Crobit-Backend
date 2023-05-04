using Microsoft.EntityFrameworkCore.Migrations;

namespace AG.Migrations
{
    public partial class NPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salts",
                table: "statuses",
                newName: "P");

            migrationBuilder.AddColumn<double>(
                name: "K",
                table: "statuses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "N",
                table: "statuses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "K",
                table: "statuses");

            migrationBuilder.DropColumn(
                name: "N",
                table: "statuses");

            migrationBuilder.RenameColumn(
                name: "P",
                table: "statuses",
                newName: "Salts");
        }
    }
}
