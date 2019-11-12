using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class AddColumnsInCandidateDetailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_GradeId",
                table: "CandidateDetails",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_OfficeId",
                table: "CandidateDetails",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_ProfessionId",
                table: "CandidateDetails",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateDetails_JobGrade_GradeId",
                table: "CandidateDetails",
                column: "GradeId",
                principalTable: "JobGrade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateDetails_OfficeDetail_OfficeId",
                table: "CandidateDetails",
                column: "OfficeId",
                principalTable: "OfficeDetail",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateDetails_ProfessionDetails_ProfessionId",
                table: "CandidateDetails",
                column: "ProfessionId",
                principalTable: "ProfessionDetails",
                principalColumn: "ProfessionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_JobGrade_GradeId",
                table: "CandidateDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_OfficeDetail_OfficeId",
                table: "CandidateDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_ProfessionDetails_ProfessionId",
                table: "CandidateDetails");

            migrationBuilder.DropIndex(
                name: "IX_CandidateDetails_GradeId",
                table: "CandidateDetails");

            migrationBuilder.DropIndex(
                name: "IX_CandidateDetails_OfficeId",
                table: "CandidateDetails");

            migrationBuilder.DropIndex(
                name: "IX_CandidateDetails_ProfessionId",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "CandidateDetails");
        }
    }
}
