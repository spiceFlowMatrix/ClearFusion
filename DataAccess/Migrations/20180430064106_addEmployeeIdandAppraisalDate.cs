using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class addEmployeeIdandAppraisalDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentAppraisalDate",
                table: "EmployeeAppraisalQuestions",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EmployeeAppraisalQuestions",
                type: "int4",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAppraisalDate",
                table: "EmployeeAppraisalQuestions");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmployeeAppraisalQuestions");
        }
    }
}
