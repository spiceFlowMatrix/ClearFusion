using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class addFieldsForAppraisal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AppraisalStatus",
                table: "EmployeeAppraisalDetails",
                type: "bool",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "EmployeeAppraisalDetails",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalScore",
                table: "EmployeeAppraisalDetails",
                type: "int4",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppraisalStatus",
                table: "EmployeeAppraisalDetails");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "EmployeeAppraisalDetails");

            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "EmployeeAppraisalDetails");
        }
    }
}
