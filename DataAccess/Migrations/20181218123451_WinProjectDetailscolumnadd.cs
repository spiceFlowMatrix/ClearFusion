using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class WinProjectDetailscolumnadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsWin",
                table: "WinProjectDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<byte[]>(
                name: "UploadedFile",
                table: "WinProjectDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedFile",
                table: "WinProjectDetails");

            migrationBuilder.AlterColumn<bool>(
                name: "IsWin",
                table: "WinProjectDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
