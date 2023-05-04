using Microsoft.EntityFrameworkCore.Migrations;

namespace AG.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapLinks_AspNetUsers_UserId",
                table: "MapLinks");

            migrationBuilder.DropIndex(
                name: "IX_MapLinks_UserId",
                table: "MapLinks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MapLinks");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "MapLinks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "MapLinks");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MapLinks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MapLinks_UserId",
                table: "MapLinks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MapLinks_AspNetUsers_UserId",
                table: "MapLinks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
