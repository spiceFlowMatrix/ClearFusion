using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePayrollTestadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePayrollDetailTest",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PayrollId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    payrollmonth = table.Column<int>(nullable: true),
                    payrollyear = table.Column<int>(nullable: true),
                    currencycode = table.Column<string>(nullable: true),
                    regcode = table.Column<string>(nullable: true),
                    basicpay = table.Column<double>(nullable: false),
                    TotalAllowance = table.Column<double>(nullable: false),
                    TotalDeduction = table.Column<double>(nullable: false),
                    Sent = table.Column<bool>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    Attendance = table.Column<int>(nullable: true),
                    Absent = table.Column<int>(nullable: true),
                    TotalDuration = table.Column<int>(nullable: true),
                    FoodAllowance = table.Column<float>(nullable: true),
                    TRAllowance = table.Column<float>(nullable: true),
                    OtherAllowance = table.Column<float>(nullable: true),
                    Medical = table.Column<float>(nullable: true),
                    Other1 = table.Column<float>(nullable: true),
                    Other2 = table.Column<float>(nullable: true),
                    AdvanceDeduction = table.Column<float>(nullable: true),
                    SalaryTaxDeduction = table.Column<float>(nullable: true),
                    FineDeduction = table.Column<float>(nullable: true),
                    OtherDeduction = table.Column<float>(nullable: true),
                    CapacityBuildingDeductibles = table.Column<float>(nullable: true),
                    SecurityDeduction = table.Column<float>(nullable: true),
                    PensionDeduction = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayrollDetailTest", x => x.PayrollId);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollDetailTest_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollDetailTest_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollDetailTest_CreatedById",
                table: "EmployeePayrollDetailTest",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollDetailTest_ModifiedById",
                table: "EmployeePayrollDetailTest",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePayrollDetailTest");
        }
    }
}
