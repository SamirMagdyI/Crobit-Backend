using Microsoft.EntityFrameworkCore.Migrations;

namespace AG.Migrations
{
    public partial class add_UserForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "statuses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "photos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_statuses_UserId",
                table: "statuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_photos_UserId",
                table: "photos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_AspNetUsers_UserId",
                table: "photos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_statuses_AspNetUsers_UserId",
                table: "statuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_AspNetUsers_UserId",
                table: "photos");

            migrationBuilder.DropForeignKey(
                name: "FK_statuses_AspNetUsers_UserId",
                table: "statuses");

            migrationBuilder.DropIndex(
                name: "IX_statuses_UserId",
                table: "statuses");

            migrationBuilder.DropIndex(
                name: "IX_photos_UserId",
                table: "photos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "statuses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "photos");
        }
    }
}
