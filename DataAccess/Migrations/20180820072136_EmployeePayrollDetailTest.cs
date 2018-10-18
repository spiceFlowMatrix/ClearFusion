using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePayrollDetailTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePayrollDetailTest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    table.PrimaryKey("PK_EmployeePayrollDetailTest", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePayrollDetailTest");
        }
    }
}
