using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class EmployeeAppraisalIdEvaluation1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeEvaluationId",
                table: "StrongandWeakPoints");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeAppraisalDetailsId",
                table: "StrongandWeakPoints",
                type: "int4",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeAppraisalDetailsId",
                table: "StrongandWeakPoints");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeEvaluationId",
                table: "StrongandWeakPoints",
                nullable: false,
                defaultValue: 0);
        }
    }
}
