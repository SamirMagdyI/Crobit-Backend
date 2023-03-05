using Microsoft.EntityFrameworkCore.Migrations;

namespace AG.Migrations
{
    public partial class delete_status_from_has_disease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hasDiseases_statuses_statusId",
                table: "hasDiseases");

            migrationBuilder.DropIndex(
                name: "IX_hasDiseases_statusId",
                table: "hasDiseases");

            migrationBuilder.DropColumn(
                name: "statusId",
                table: "hasDiseases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "statusId",
                table: "hasDiseases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_hasDiseases_statusId",
                table: "hasDiseases",
                column: "statusId");

            migrationBuilder.AddForeignKey(
                name: "FK_hasDiseases_statuses_statusId",
                table: "hasDiseases",
                column: "statusId",
                principalTable: "statuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
