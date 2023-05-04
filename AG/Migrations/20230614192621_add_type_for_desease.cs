using Microsoft.EntityFrameworkCore.Migrations;

namespace AG.Migrations
{
    public partial class add_type_for_desease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_location_AspNetUsers_UserId",
                table: "location");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "location",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Diseases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_location_AspNetUsers_UserId",
                table: "location",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_location_AspNetUsers_UserId",
                table: "location");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Diseases");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "location",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_location_AspNetUsers_UserId",
                table: "location",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
