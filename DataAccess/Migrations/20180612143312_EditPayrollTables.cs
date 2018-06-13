using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EditPayrollTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePayrollForMonth",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeePaymentTypesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    OfficeId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    FinancialYearDate = table.Column<DateTime>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true),
                    PaymentType = table.Column<int>(nullable: false),
                    WorkingDays = table.Column<int>(nullable: false),
                    PresentDays = table.Column<int>(nullable: false),
                    AbsentDays = table.Column<int>(nullable: false),
                    LeaveDays = table.Column<int>(nullable: false),
                    TotalWorkHours = table.Column<int>(nullable: false),
                    HourlyRate = table.Column<double>(nullable: true),
                    TotalGeneralAmount = table.Column<double>(nullable: true),
                    TotalAllowance = table.Column<double>(nullable: true),
                    TotalDeduction = table.Column<double>(nullable: true),
                    GrossSalary = table.Column<double>(nullable: true),
                    OverTimeHours = table.Column<double>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    PensionRate = table.Column<double>(nullable: true),
                    PensionAmount = table.Column<double>(nullable: true),
                    SalaryTax = table.Column<double>(nullable: true),
                    NetSalary = table.Column<double>(nullable: true),
                    AdvanceAmount = table.Column<double>(nullable: false),
                    IsAdvanceApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayrollForMonth", x => x.EmployeePaymentTypesId);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollForMonth_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollForMonth_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollForMonth_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayrollMonth",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MonthlyPayrollId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeID = table.Column<int>(nullable: false),
                    SalaryHeadId = table.Column<int>(nullable: false),
                    MonthlyAmount = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayrollMonth", x => x.MonthlyPayrollId);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollMonth_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollMonth_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollMonth_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollMonth_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollMonth_SalaryHeadDetails_SalaryHeadId",
                        column: x => x.SalaryHeadId,
                        principalTable: "SalaryHeadDetails",
                        principalColumn: "SalaryHeadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollForMonth_CreatedById",
                table: "EmployeePayrollForMonth",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollForMonth_EmployeeID",
                table: "EmployeePayrollForMonth",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollForMonth_ModifiedById",
                table: "EmployeePayrollForMonth",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_CreatedById",
                table: "EmployeePayrollMonth",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_CurrencyId",
                table: "EmployeePayrollMonth",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_EmployeeID",
                table: "EmployeePayrollMonth",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_ModifiedById",
                table: "EmployeePayrollMonth",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_SalaryHeadId",
                table: "EmployeePayrollMonth",
                column: "SalaryHeadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePayrollForMonth");

            migrationBuilder.DropTable(
                name: "EmployeePayrollMonth");
        }
    }
}
