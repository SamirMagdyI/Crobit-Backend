using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AG.Migrations
{
    public partial class change_photoToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDiseas",
                table: "photos");

            migrationBuilder.AlterColumn<string>(
                name: "photo",
                table: "photos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "photo",
                table: "photos",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasDiseas",
                table: "photos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
