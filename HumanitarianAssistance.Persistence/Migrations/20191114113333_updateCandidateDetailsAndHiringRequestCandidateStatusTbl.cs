using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateCandidateDetailsAndHiringRequestCandidateStatusTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_ProjectHiringRequestDetail_HiringRequestId",
                table: "CandidateDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_ProjectDetail_ProjectId",
                table: "CandidateDetails");

            migrationBuilder.DropIndex(
                name: "IX_CandidateDetails_HiringRequestId",
                table: "CandidateDetails");

            migrationBuilder.DropIndex(
                name: "IX_CandidateDetails_ProjectId",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "HiringRequestId",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "CandidateDetails");

            migrationBuilder.AddColumn<long>(
                name: "TransportTypeEntityId",
                table: "StoreLogger",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "HiringRequestId",
                table: "HiringRequestCandidateStatus",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "HiringRequestCandidateStatus",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidateStatus_HiringRequestId",
                table: "HiringRequestCandidateStatus",
                column: "HiringRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidateStatus_ProjectId",
                table: "HiringRequestCandidateStatus",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_HiringRequestCandidateStatus_ProjectHiringRequestDetail_Hir~",
                table: "HiringRequestCandidateStatus",
                column: "HiringRequestId",
                principalTable: "ProjectHiringRequestDetail",
                principalColumn: "HiringRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HiringRequestCandidateStatus_ProjectDetail_ProjectId",
                table: "HiringRequestCandidateStatus",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiringRequestCandidateStatus_ProjectHiringRequestDetail_Hir~",
                table: "HiringRequestCandidateStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_HiringRequestCandidateStatus_ProjectDetail_ProjectId",
                table: "HiringRequestCandidateStatus");

            migrationBuilder.DropIndex(
                name: "IX_HiringRequestCandidateStatus_HiringRequestId",
                table: "HiringRequestCandidateStatus");

            migrationBuilder.DropIndex(
                name: "IX_HiringRequestCandidateStatus_ProjectId",
                table: "HiringRequestCandidateStatus");

            migrationBuilder.DropColumn(
                name: "TransportTypeEntityId",
                table: "StoreLogger");

            migrationBuilder.DropColumn(
                name: "HiringRequestId",
                table: "HiringRequestCandidateStatus");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "HiringRequestCandidateStatus");

            migrationBuilder.AddColumn<long>(
                name: "HiringRequestId",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "CandidateDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_HiringRequestId",
                table: "CandidateDetails",
                column: "HiringRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_ProjectId",
                table: "CandidateDetails",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateDetails_ProjectHiringRequestDetail_HiringRequestId",
                table: "CandidateDetails",
                column: "HiringRequestId",
                principalTable: "ProjectHiringRequestDetail",
                principalColumn: "HiringRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateDetails_ProjectDetail_ProjectId",
                table: "CandidateDetails",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
