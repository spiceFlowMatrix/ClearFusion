using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EMployeePayrollTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePayrollDetailTest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePayrollDetailTest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Absent = table.Column<int>(nullable: true),
                    AdvanceDeduction = table.Column<float>(nullable: true),
                    Attendance = table.Column<int>(nullable: true),
                    CapacityBuildingDeductibles = table.Column<float>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    FineDeduction = table.Column<float>(nullable: true),
                    FoodAllowance = table.Column<float>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Medical = table.Column<float>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Other1 = table.Column<float>(nullable: true),
                    Other2 = table.Column<float>(nullable: true),
                    OtherAllowance = table.Column<float>(nullable: true),
                    OtherDeduction = table.Column<float>(nullable: true),
                    PensionDeduction = table.Column<float>(nullable: true),
                    SalaryTaxDeduction = table.Column<float>(nullable: true),
                    SecurityDeduction = table.Column<float>(nullable: true),
                    Sent = table.Column<bool>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    TRAllowance = table.Column<float>(nullable: true),
                    TotalAllowance = table.Column<double>(nullable: false),
                    TotalDeduction = table.Column<double>(nullable: false),
                    TotalDuration = table.Column<int>(nullable: true),
                    basicpay = table.Column<double>(nullable: false),
                    currencycode = table.Column<string>(nullable: true),
                    payrollmonth = table.Column<int>(nullable: true),
                    payrollyear = table.Column<int>(nullable: true),
                    regcode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayrollDetailTest", x => x.Id);
                });
        }
    }
}
