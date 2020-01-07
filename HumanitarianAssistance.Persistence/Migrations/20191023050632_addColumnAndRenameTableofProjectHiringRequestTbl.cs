using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addColumnAndRenameTableofProjectHiringRequestTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobHiringDetails_ProjectHiringRequestDetail_HiringRequestId",
                table: "JobHiringDetails");

            migrationBuilder.DropTable(
                name: "JobHiringDetail");

            migrationBuilder.DropIndex(
                name: "IX_JobHiringDetails_HiringRequestId",
                table: "JobHiringDetails");

            migrationBuilder.AddColumn<long>(
                name: "JobId",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectJobHiringDetail",
                columns: table => new
                {
                    JobId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    JobCode = table.Column<string>(maxLength: 50, nullable: true),
                    JobDescription = table.Column<string>(nullable: true),
                    ProfessionId = table.Column<int>(nullable: true),
                    TotalVacancies = table.Column<int>(nullable: true),
                    PayRate = table.Column<double>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false),
                    GradeId = table.Column<int>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectJobHiringDetail", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_ProjectJobHiringDetail_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectJobHiringDetail_JobGrade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "JobGrade",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectJobHiringDetail_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectJobHiringDetail_ProfessionDetails_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "ProfessionDetails",
                        principalColumn: "ProfessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectJobHiringDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_JobId",
                table: "ProjectHiringRequestDetail",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobHiringDetail_CurrencyId",
                table: "ProjectJobHiringDetail",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobHiringDetail_GradeId",
                table: "ProjectJobHiringDetail",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobHiringDetail_OfficeId",
                table: "ProjectJobHiringDetail",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobHiringDetail_ProfessionId",
                table: "ProjectJobHiringDetail",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobHiringDetail_ProjectId",
                table: "ProjectJobHiringDetail",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHiringRequestDetail_ProjectJobHiringDetail_JobId",
                table: "ProjectHiringRequestDetail",
                column: "JobId",
                principalTable: "ProjectJobHiringDetail",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHiringRequestDetail_ProjectJobHiringDetail_JobId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropTable(
                name: "ProjectJobHiringDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectHiringRequestDetail_JobId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.CreateTable(
                name: "JobHiringDetail",
                columns: table => new
                {
                    JobId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    GradeId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    JobCode = table.Column<string>(maxLength: 50, nullable: true),
                    JobDescription = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false),
                    PayRate = table.Column<double>(nullable: true),
                    ProfessionId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    TotalVacancies = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobHiringDetail", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_JobHiringDetail_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobHiringDetail_JobGrade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "JobGrade",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobHiringDetail_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobHiringDetail_ProfessionDetails_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "ProfessionDetails",
                        principalColumn: "ProfessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobHiringDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetails_HiringRequestId",
                table: "JobHiringDetails",
                column: "HiringRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetail_CurrencyId",
                table: "JobHiringDetail",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetail_GradeId",
                table: "JobHiringDetail",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetail_OfficeId",
                table: "JobHiringDetail",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetail_ProfessionId",
                table: "JobHiringDetail",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetail_ProjectId",
                table: "JobHiringDetail",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobHiringDetails_ProjectHiringRequestDetail_HiringRequestId",
                table: "JobHiringDetails",
                column: "HiringRequestId",
                principalTable: "ProjectHiringRequestDetail",
                principalColumn: "HiringRequestId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
