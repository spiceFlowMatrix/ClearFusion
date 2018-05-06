using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class foreignKeyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExistInterviewDetails_EmployeeDetail_EmployeeId",
                table: "ExistInterviewDetails");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "ExistInterviewDetails",
                newName: "EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_ExistInterviewDetails_EmployeeId",
                table: "ExistInterviewDetails",
                newName: "IX_ExistInterviewDetails_EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExistInterviewDetails_EmployeeDetail_EmployeeID",
                table: "ExistInterviewDetails",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExistInterviewDetails_EmployeeDetail_EmployeeID",
                table: "ExistInterviewDetails");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "ExistInterviewDetails",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_ExistInterviewDetails_EmployeeID",
                table: "ExistInterviewDetails",
                newName: "IX_ExistInterviewDetails_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExistInterviewDetails_EmployeeDetail_EmployeeId",
                table: "ExistInterviewDetails",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
