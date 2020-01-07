using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addTableJobHiringDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobHiringDetail",
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
                    PayRate = table.Column<int>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false),
                    GradeId = table.Column<int>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true)
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
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobHiringDetail");
        }
    }
}
