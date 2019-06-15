using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AttendanceGroupMasterTableAdd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "AttendanceGroupMaster",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AttendanceGroupMaster",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AttendanceGroupMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "AttendanceGroupMaster",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "AttendanceGroupMaster",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceGroupMaster_CreatedById",
                table: "AttendanceGroupMaster",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceGroupMaster_ModifiedById",
                table: "AttendanceGroupMaster",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceGroupMaster_AspNetUsers_CreatedById",
                table: "AttendanceGroupMaster",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceGroupMaster_AspNetUsers_ModifiedById",
                table: "AttendanceGroupMaster",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceGroupMaster_AspNetUsers_CreatedById",
                table: "AttendanceGroupMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceGroupMaster_AspNetUsers_ModifiedById",
                table: "AttendanceGroupMaster");

            migrationBuilder.DropIndex(
                name: "IX_AttendanceGroupMaster_CreatedById",
                table: "AttendanceGroupMaster");

            migrationBuilder.DropIndex(
                name: "IX_AttendanceGroupMaster_ModifiedById",
                table: "AttendanceGroupMaster");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "AttendanceGroupMaster");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AttendanceGroupMaster");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AttendanceGroupMaster");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "AttendanceGroupMaster");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "AttendanceGroupMaster");
        }
    }
}
