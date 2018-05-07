using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class InterviewTechnicalQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterviewTechnicalQuestion_TechnicalQuestion_TechnicalQuestionId",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropIndex(
                name: "IX_InterviewTechnicalQuestion_TechnicalQuestionId",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropColumn(
                name: "TechnicalQuestionId",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "InterviewTechnicalQuestion",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Question",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.AddColumn<int>(
                name: "TechnicalQuestionId",
                table: "InterviewTechnicalQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestion_TechnicalQuestionId",
                table: "InterviewTechnicalQuestion",
                column: "TechnicalQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewTechnicalQuestion_TechnicalQuestion_TechnicalQuestionId",
                table: "InterviewTechnicalQuestion",
                column: "TechnicalQuestionId",
                principalTable: "TechnicalQuestion",
                principalColumn: "TechnicalQuestionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
