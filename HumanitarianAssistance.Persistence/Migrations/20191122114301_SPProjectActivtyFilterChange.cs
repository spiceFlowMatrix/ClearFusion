using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class SPProjectActivtyFilterChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobType",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.AlterColumn<double>(
                name: "TotalMarksObtain",
                table: "ProjectInterviewDetails",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "EducationDegreeId",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HourlyRate",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobTypeId",
                table: "ProjectHiringRequestDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_EducationDegreeId",
                table: "ProjectHiringRequestDetail",
                column: "EducationDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_PositionId",
                table: "ProjectHiringRequestDetail",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHiringRequestDetail_EducationDegreeMaster_EducationD~",
                table: "ProjectHiringRequestDetail",
                column: "EducationDegreeId",
                principalTable: "EducationDegreeMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHiringRequestDetail_DesignationDetail_PositionId",
                table: "ProjectHiringRequestDetail",
                column: "PositionId",
                principalTable: "DesignationDetail",
                principalColumn: "DesignationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"SQL/sp_getprojectactivitylistfilter.sql")));

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHiringRequestDetail_EducationDegreeMaster_EducationD~",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHiringRequestDetail_DesignationDetail_PositionId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectHiringRequestDetail_EducationDegreeId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectHiringRequestDetail_PositionId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "EducationDegreeId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "JobTypeId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.AlterColumn<int>(
                name: "TotalMarksObtain",
                table: "ProjectInterviewDetails",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "JobType",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "ProjectHiringRequestDetail",
                nullable: true);
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_project_projectactivitylist_filter(bigint, text, text, text, text, text, integer[], bigint[], boolean, boolean, boolean, integer, integer, integer, integer, integer, integer, boolean, boolean, boolean, integer)");

        }
    }
}
