using Microsoft.EntityFrameworkCore.Migrations;

namespace AG.Migrations
{
    public partial class pointTaple : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "point",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lan = table.Column<double>(type: "float", nullable: false),
                    Wan = table.Column<double>(type: "float", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_point", x => x.Id);
                    table.ForeignKey(
                        name: "FK_point_location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_point_LocationID",
                table: "point",
                column: "LocationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "point");
        }
    }
}
