using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class EmployeeInterviewModelCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CandidateName",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "CandidatePosition",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "DutyStation",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "ResidingProvince",
                table: "InterviewDetails");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "InterviewDetails",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "JobId",
                table: "InterviewDetails",
                type: "int8",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "InterviewDetails");

            migrationBuilder.AddColumn<string>(
                name: "CandidateName",
                table: "InterviewDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CandidatePosition",
                table: "InterviewDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "InterviewDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DutyStation",
                table: "InterviewDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "InterviewDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "InterviewDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResidingProvince",
                table: "InterviewDetails",
                nullable: true);
        }
    }
}
