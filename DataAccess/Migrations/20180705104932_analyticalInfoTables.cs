using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class analyticalInfoTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MembershipSupportInPoliticalParty",
                table: "EmployeeProfessionalDetail",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryAnalyticalInfo",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeSalaryAnalyticalInfoId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountCode = table.Column<int>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    BudgetLineId = table.Column<long>(nullable: false),
                    SalaryPercentage = table.Column<double>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryAnalyticalInfo", x => x.EmployeeSalaryAnalyticalInfoId);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_ProjectBudgetLine_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLine",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_ProjectDetails_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetails",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_BudgetLineId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_CreatedById",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_EmployeeID",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_ModifiedById",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_ProjectId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeSalaryAnalyticalInfo");

            migrationBuilder.DropColumn(
                name: "MembershipSupportInPoliticalParty",
                table: "EmployeeProfessionalDetail");
        }
    }
}
