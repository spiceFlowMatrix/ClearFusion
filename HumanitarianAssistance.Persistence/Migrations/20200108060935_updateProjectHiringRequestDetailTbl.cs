using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateProjectHiringRequestDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobCategoryId",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_JobCategoryId",
                table: "ProjectHiringRequestDetail",
                column: "JobCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHiringRequestDetail_Department_JobCategoryId",
                table: "ProjectHiringRequestDetail",
                column: "JobCategoryId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHiringRequestDetail_Department_JobCategoryId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectHiringRequestDetail_JobCategoryId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "JobCategoryId",
                table: "ProjectHiringRequestDetail");
        }
    }
}
