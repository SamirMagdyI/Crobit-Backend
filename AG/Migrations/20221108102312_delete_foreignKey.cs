using Microsoft.EntityFrameworkCore.Migrations;

namespace AG.Migrations
{
    public partial class delete_foreignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_AspNetUsers_UserId1",
                table: "photos");

            migrationBuilder.DropForeignKey(
                name: "FK_statuses_AspNetUsers_UserId1",
                table: "statuses");

            migrationBuilder.DropIndex(
                name: "IX_statuses_UserId1",
                table: "statuses");

            migrationBuilder.DropIndex(
                name: "IX_photos_UserId1",
                table: "photos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "statuses");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "statuses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "photos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "photos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "statuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "statuses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "photos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "photos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_statuses_UserId1",
                table: "statuses",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_photos_UserId1",
                table: "photos",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_AspNetUsers_UserId1",
                table: "photos",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_statuses_AspNetUsers_UserId1",
                table: "statuses",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
