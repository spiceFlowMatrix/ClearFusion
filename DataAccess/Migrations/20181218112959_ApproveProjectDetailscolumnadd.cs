using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ApproveProjectDetailscolumnadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "ApproveProjectDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<byte[]>(
                name: "UploadedFile",
                table: "ApproveProjectDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedFile",
                table: "ApproveProjectDetails");

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "ApproveProjectDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
