using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AG.Migrations
{
    public partial class mani_taples : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SugestedTreatment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    HasDiseas = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_photos_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    WaterLevel = table.Column<int>(type: "int", nullable: false),
                    Salts = table.Column<int>(type: "int", nullable: false),
                    Ph = table.Column<int>(type: "int", nullable: false),
                    Heat = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_statuses_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "hasDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    statusId = table.Column<int>(type: "int", nullable: false),
                    photoId = table.Column<int>(type: "int", nullable: false),
                    DiseaseID = table.Column<int>(type: "int", nullable: false),
                    diseasesID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hasDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hasDiseases_Diseases_diseasesID",
                        column: x => x.diseasesID,
                        principalTable: "Diseases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_hasDiseases_photos_photoId",
                        column: x => x.photoId,
                        principalTable: "photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hasDiseases_statuses_statusId",
                        column: x => x.statusId,
                        principalTable: "statuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PageNumper = table.Column<int>(type: "int", nullable: false),
                    HasDiseaseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_notifications_hasDiseases_HasDiseaseID",
                        column: x => x.HasDiseaseID,
                        principalTable: "hasDiseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hasDiseases_diseasesID",
                table: "hasDiseases",
                column: "diseasesID");

            migrationBuilder.CreateIndex(
                name: "IX_hasDiseases_photoId",
                table: "hasDiseases",
                column: "photoId");

            migrationBuilder.CreateIndex(
                name: "IX_hasDiseases_statusId",
                table: "hasDiseases",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_HasDiseaseID",
                table: "notifications",
                column: "HasDiseaseID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_photos_UserId1",
                table: "photos",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_statuses_UserId1",
                table: "statuses",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "hasDiseases");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "photos");

            migrationBuilder.DropTable(
                name: "statuses");
        }
    }
}
