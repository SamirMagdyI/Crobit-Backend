using Microsoft.EntityFrameworkCore.Migrations;

namespace AG.Migrations
{
    public partial class editHasDisease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hasDiseases_Diseases_diseasesID",
                table: "hasDiseases");

            migrationBuilder.DropColumn(
                name: "DiseaseID",
                table: "hasDiseases");

            migrationBuilder.RenameColumn(
                name: "diseasesID",
                table: "hasDiseases",
                newName: "DiseasesID");

            migrationBuilder.RenameIndex(
                name: "IX_hasDiseases_diseasesID",
                table: "hasDiseases",
                newName: "IX_hasDiseases_DiseasesID");

            migrationBuilder.AlterColumn<int>(
                name: "DiseasesID",
                table: "hasDiseases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_hasDiseases_Diseases_DiseasesID",
                table: "hasDiseases",
                column: "DiseasesID",
                principalTable: "Diseases",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hasDiseases_Diseases_DiseasesID",
                table: "hasDiseases");

            migrationBuilder.RenameColumn(
                name: "DiseasesID",
                table: "hasDiseases",
                newName: "diseasesID");

            migrationBuilder.RenameIndex(
                name: "IX_hasDiseases_DiseasesID",
                table: "hasDiseases",
                newName: "IX_hasDiseases_diseasesID");

            migrationBuilder.AlterColumn<int>(
                name: "diseasesID",
                table: "hasDiseases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DiseaseID",
                table: "hasDiseases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_hasDiseases_Diseases_diseasesID",
                table: "hasDiseases",
                column: "diseasesID",
                principalTable: "Diseases",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
