using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class finalInterviewDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CurrentBase",
                table: "InterviewDetails",
                type: "int8",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "CurrentMeal",
                table: "InterviewDetails",
                type: "bool",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "CurrentOther",
                table: "InterviewDetails",
                type: "int8",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "CurrentTransportation",
                table: "InterviewDetails",
                type: "bool",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ExpectationBase",
                table: "InterviewDetails",
                type: "int8",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "ExpectationMeal",
                table: "InterviewDetails",
                type: "bool",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ExpectationOther",
                table: "InterviewDetails",
                type: "int8",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "ExpectationTransportation",
                table: "InterviewDetails",
                type: "bool",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Interviewer1",
                table: "InterviewDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interviewer2",
                table: "InterviewDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interviewer3",
                table: "InterviewDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interviewer4",
                table: "InterviewDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "InterviewDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalMarksObtained",
                table: "InterviewDetails",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBase",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "CurrentMeal",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "CurrentOther",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "CurrentTransportation",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "ExpectationBase",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "ExpectationMeal",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "ExpectationOther",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "ExpectationTransportation",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "Interviewer1",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "Interviewer2",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "Interviewer3",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "Interviewer4",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "InterviewDetails");

            migrationBuilder.DropColumn(
                name: "TotalMarksObtained",
                table: "InterviewDetails");
        }
    }
}
