using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectActivityDetail270219 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityStatusDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    StatusId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityStatusDetail", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_ActivityStatusDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityStatusDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectActivityDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ActivityId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActivityName = table.Column<string>(nullable: true),
                    ActivityDescription = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    BudgetLineId = table.Column<long>(nullable: true),
                    AssigneeId = table.Column<int>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    Recurring = table.Column<bool>(nullable: true),
                    RecurringCount = table.Column<int>(nullable: true),
                    RecurrinTypeId = table.Column<int>(nullable: false),
                    ImplementationProgress = table.Column<int>(nullable: true),
                    ImplementationStatus = table.Column<bool>(nullable: true),
                    ActualStartDate = table.Column<DateTime>(nullable: true),
                    ActualEndDate = table.Column<DateTime>(nullable: true),
                    ImplementationMethod = table.Column<string>(nullable: true),
                    ImplementationChalanges = table.Column<string>(nullable: true),
                    OvercomingChallanges = table.Column<string>(nullable: true),
                    ExtensionStartDate = table.Column<DateTime>(nullable: true),
                    ExtensionEndDate = table.Column<DateTime>(nullable: true),
                    MonitoringProgress = table.Column<int>(nullable: true),
                    MonitoringStatus = table.Column<bool>(nullable: true),
                    MonitoringScore = table.Column<int>(nullable: true),
                    MonitoringFrequency = table.Column<int>(nullable: true),
                    VerificationSource = table.Column<string>(nullable: true),
                    Strengths = table.Column<string>(nullable: true),
                    Weeknesses = table.Column<string>(nullable: true),
                    MonitoringChallenges = table.Column<string>(nullable: true),
                    Recommendation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivityDetail", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_ProjectActivityDetail_ProjectBudgetLineDetail_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLineDetail",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityDetail_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityDetail_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityDetail_ActivityStatusDetail_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ActivityStatusDetail",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityStatusDetail_CreatedById",
                table: "ActivityStatusDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityStatusDetail_ModifiedById",
                table: "ActivityStatusDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_BudgetLineId",
                table: "ProjectActivityDetail",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_CreatedById",
                table: "ProjectActivityDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_EmployeeID",
                table: "ProjectActivityDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_ModifiedById",
                table: "ProjectActivityDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_OfficeId",
                table: "ProjectActivityDetail",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_StatusId",
                table: "ProjectActivityDetail",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectActivityDetail");

            migrationBuilder.DropTable(
                name: "ActivityStatusDetail");
        }
    }
}
