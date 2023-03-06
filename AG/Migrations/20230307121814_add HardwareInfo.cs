using Microsoft.EntityFrameworkCore.Migrations;

namespace AG.Migrations
{
    public partial class addHardwareInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_AspNetUsers_UserId",
                table: "photos");

            migrationBuilder.DropForeignKey(
                name: "FK_statuses_AspNetUsers_UserId",
                table: "statuses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "statuses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "photos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "hardwareInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HardwareNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hardwareInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hardwareInfo_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hardwareInfo_UserId",
                table: "hardwareInfo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_AspNetUsers_UserId",
                table: "photos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_statuses_AspNetUsers_UserId",
                table: "statuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_AspNetUsers_UserId",
                table: "photos");

            migrationBuilder.DropForeignKey(
                name: "FK_statuses_AspNetUsers_UserId",
                table: "statuses");

            migrationBuilder.DropTable(
                name: "hardwareInfo");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "statuses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "photos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
