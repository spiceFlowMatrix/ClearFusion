using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addNewTableHiringRequestCandidateStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CandidateStatus",
                table: "CandidateDetails");

            migrationBuilder.RenameColumn(
                name: "InterviewId",
                table: "CandidateDetails",
                newName: "HiringRequestId");

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "CandidateDetails",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HiringRequestCandidateStatus",
                columns: table => new
                {
                    CandidateStatusId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CandidateId = table.Column<long>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    CandidateStatus = table.Column<int>(nullable: false),
                    InterviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringRequestCandidateStatus", x => x.CandidateStatusId);
                    table.ForeignKey(
                        name: "FK_HiringRequestCandidateStatus_CandidateDetails_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "CandidateDetails",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HiringRequestCandidateStatus_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_HiringRequestId",
                table: "CandidateDetails",
                column: "HiringRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_ProjectId",
                table: "CandidateDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidateStatus_CandidateId",
                table: "HiringRequestCandidateStatus",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidateStatus_EmployeeID",
                table: "HiringRequestCandidateStatus",
                column: "EmployeeID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_ProjectHiringRequestDetail_HiringRequestId",
                table: "CandidateDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_ProjectDetail_ProjectId",
                table: "CandidateDetails");

            migrationBuilder.DropTable(
                name: "HiringRequestCandidateStatus");

            migrationBuilder.DropIndex(
                name: "IX_CandidateDetails_HiringRequestId",
                table: "CandidateDetails");

            migrationBuilder.DropIndex(
                name: "IX_CandidateDetails_ProjectId",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "CandidateDetails");

            migrationBuilder.RenameColumn(
                name: "HiringRequestId",
                table: "CandidateDetails",
                newName: "InterviewId");

            migrationBuilder.AddColumn<int>(
                name: "CandidateStatus",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0);
        }
    }
}
