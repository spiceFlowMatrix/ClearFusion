using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectHiringRequestDetail12062019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectHiringRequestDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    HiringReuestId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HiringRequestCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Profession = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    TotalVacancies = table.Column<int>(nullable: true),
                    FilledVacancies = table.Column<int>(nullable: true),
                    BasicPay = table.Column<double>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    BudgetLineId = table.Column<long>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    GradeId = table.Column<int>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectHiringRequestDetail", x => x.HiringReuestId);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_ProjectBudgetLineDetail_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLineDetail",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_JobGrade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "JobGrade",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_BudgetLineId",
                table: "ProjectHiringRequestDetail",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_CreatedById",
                table: "ProjectHiringRequestDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_CurrencyId",
                table: "ProjectHiringRequestDetail",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_EmployeeID",
                table: "ProjectHiringRequestDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_GradeId",
                table: "ProjectHiringRequestDetail",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_ModifiedById",
                table: "ProjectHiringRequestDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_OfficeId",
                table: "ProjectHiringRequestDetail",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_ProjectId",
                table: "ProjectHiringRequestDetail",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectHiringRequestDetail");
        }
    }
}
