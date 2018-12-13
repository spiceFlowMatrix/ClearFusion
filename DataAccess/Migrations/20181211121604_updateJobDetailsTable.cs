using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateJobDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobDetails_JobPhases_JobPhaseId",
                table: "JobDetails");

            migrationBuilder.AlterColumn<long>(
                name: "JobPhaseId",
                table: "JobDetails",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_JobDetails_JobPhases_JobPhaseId",
                table: "JobDetails",
                column: "JobPhaseId",
                principalTable: "JobPhases",
                principalColumn: "JobPhaseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobDetails_JobPhases_JobPhaseId",
                table: "JobDetails");

            migrationBuilder.AlterColumn<long>(
                name: "JobPhaseId",
                table: "JobDetails",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobDetails_JobPhases_JobPhaseId",
                table: "JobDetails",
                column: "JobPhaseId",
                principalTable: "JobPhases",
                principalColumn: "JobPhaseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
