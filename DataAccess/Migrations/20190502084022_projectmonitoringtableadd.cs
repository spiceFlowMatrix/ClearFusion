using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class projectmonitoringtableadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectMonitoringReviewDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectMonitoringReviewId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PostivePoints = table.Column<string>(nullable: true),
                    NegativePoints = table.Column<string>(nullable: true),
                    Recommendations = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    ActivityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMonitoringReviewDetail", x => x.ProjectMonitoringReviewId);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringReviewDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringReviewDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMonitoringIndicatorDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    MonitoringIndicatorId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectIndicatorId = table.Column<long>(nullable: false),
                    ProjectMonitoringReviewId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMonitoringIndicatorDetail", x => x.MonitoringIndicatorId);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorDetail_ProjectMonitoringReviewDetail_ProjectMonitoringReviewId",
                        column: x => x.ProjectMonitoringReviewId,
                        principalTable: "ProjectMonitoringReviewDetail",
                        principalColumn: "ProjectMonitoringReviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMonitoringIndicatorQuestions",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    QuestionId = table.Column<long>(nullable: false),
                    Verification = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: true),
                    MonitoringIndicatorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMonitoringIndicatorQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorQuestions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorQuestions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorQuestions_ProjectMonitoringIndicatorDetail_MonitoringIndicatorId",
                        column: x => x.MonitoringIndicatorId,
                        principalTable: "ProjectMonitoringIndicatorDetail",
                        principalColumn: "MonitoringIndicatorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorDetail_CreatedById",
                table: "ProjectMonitoringIndicatorDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorDetail_ModifiedById",
                table: "ProjectMonitoringIndicatorDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorDetail_ProjectMonitoringReviewId",
                table: "ProjectMonitoringIndicatorDetail",
                column: "ProjectMonitoringReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_CreatedById",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_ModifiedById",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_MonitoringIndicatorId",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "MonitoringIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringReviewDetail_CreatedById",
                table: "ProjectMonitoringReviewDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringReviewDetail_ModifiedById",
                table: "ProjectMonitoringReviewDetail",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.DropTable(
                name: "ProjectMonitoringIndicatorDetail");

            migrationBuilder.DropTable(
                name: "ProjectMonitoringReviewDetail");
        }
    }
}
