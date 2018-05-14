using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class EmployeeAppraisalQuestionsModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "EmployeeAppraisalQuestions",
                type: "int4",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "EmployeeAppraisalQuestions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4",
                oldNullable: true);
        }
    }
}
