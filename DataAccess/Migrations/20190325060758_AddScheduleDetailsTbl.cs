using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddScheduleDetailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ScheduleId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ScheduleType = table.Column<string>(nullable: true),
                    ScheduleCode = table.Column<string>(nullable: true),
                    PolicyId = table.Column<long>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    JobId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    Monday = table.Column<bool>(nullable: false),
                    Tuesday = table.Column<bool>(nullable: false),
                    Wednesday = table.Column<bool>(nullable: false),
                    Thursday = table.Column<bool>(nullable: false),
                    Friday = table.Column<bool>(nullable: false),
                    Saturday = table.Column<bool>(nullable: false),
                    Sunday = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDetails", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_ScheduleDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleDetails_JobDetails_JobId",
                        column: x => x.JobId,
                        principalTable: "JobDetails",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleDetails_PolicyDetails_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "PolicyDetails",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleDetails_ProjectDetails_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetails",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_CreatedById",
                table: "ScheduleDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_JobId",
                table: "ScheduleDetails",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_ModifiedById",
                table: "ScheduleDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_PolicyId",
                table: "ScheduleDetails",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_ProjectId",
                table: "ScheduleDetails",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleDetails");
        }
    }
}
