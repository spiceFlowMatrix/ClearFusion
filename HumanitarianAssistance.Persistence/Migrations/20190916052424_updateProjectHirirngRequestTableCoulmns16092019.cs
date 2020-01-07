using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateProjectHirirngRequestTableCoulmns16092019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AnouncingDate",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosingDate",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractDuration",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobCategory",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobStatus",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobType",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KnowladgeAndSkillRequired",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinimumEducationLevel",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProviceId",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceDetailsProvinceId",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalaryRange",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Shift",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecificDutiesAndResponsblities",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmissionGuidlines",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_CountryId",
                table: "ProjectHiringRequestDetail",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_ProvinceDetailsProvinceId",
                table: "ProjectHiringRequestDetail",
                column: "ProvinceDetailsProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHiringRequestDetail_CountryDetails_CountryId",
                table: "ProjectHiringRequestDetail",
                column: "CountryId",
                principalTable: "CountryDetails",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHiringRequestDetail_ProvinceDetails_ProvinceDetailsP~",
                table: "ProjectHiringRequestDetail",
                column: "ProvinceDetailsProvinceId",
                principalTable: "ProvinceDetails",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHiringRequestDetail_CountryDetails_CountryId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHiringRequestDetail_ProvinceDetails_ProvinceDetailsP~",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectHiringRequestDetail_CountryId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectHiringRequestDetail_ProvinceDetailsProvinceId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "AnouncingDate",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "Background",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "ClosingDate",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "ContractDuration",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "JobCategory",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "JobStatus",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "KnowladgeAndSkillRequired",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "MinimumEducationLevel",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "Organization",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "ProviceId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "ProvinceDetailsProvinceId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "SalaryRange",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "Shift",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "SpecificDutiesAndResponsblities",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "SubmissionGuidlines",
                table: "ProjectHiringRequestDetail");
        }
    }
}
