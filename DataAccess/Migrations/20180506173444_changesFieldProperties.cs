using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class changesFieldProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excellent",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropColumn(
                name: "Fair",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropColumn(
                name: "Good",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropColumn(
                name: "Perfect",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropColumn(
                name: "Poor",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "InterviewTechnicalQuestion",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.AddColumn<int>(
                name: "Excellent",
                table: "InterviewTechnicalQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fair",
                table: "InterviewTechnicalQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Good",
                table: "InterviewTechnicalQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Perfect",
                table: "InterviewTechnicalQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Poor",
                table: "InterviewTechnicalQuestion",
                nullable: false,
                defaultValue: 0);
        }
    }
}
