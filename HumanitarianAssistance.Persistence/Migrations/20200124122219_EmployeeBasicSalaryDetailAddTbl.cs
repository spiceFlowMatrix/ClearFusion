using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class EmployeeBasicSalaryDetailAddTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayrollInfoDetail_CurrencyDetails_CurrencyId",
                table: "EmployeePayrollInfoDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePayrollInfoDetail_CurrencyId",
                table: "EmployeePayrollInfoDetail");

            migrationBuilder.DropColumn(
                name: "BasicSalary",
                table: "EmployeePayrollInfoDetail");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "EmployeePayrollInfoDetail",
                newName: "Year");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "EmployeePayrollInfoDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EmployeeBasicSalaryDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    BasicSalary = table.Column<double>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBasicSalaryDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBasicSalaryDetail_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeBasicSalaryDetail_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBasicSalaryDetail_CurrencyId",
                table: "EmployeeBasicSalaryDetail",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBasicSalaryDetail_EmployeeId",
                table: "EmployeeBasicSalaryDetail",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeBasicSalaryDetail");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "EmployeePayrollInfoDetail");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "EmployeePayrollInfoDetail",
                newName: "CurrencyId");

            migrationBuilder.AddColumn<double>(
                name: "BasicSalary",
                table: "EmployeePayrollInfoDetail",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollInfoDetail_CurrencyId",
                table: "EmployeePayrollInfoDetail",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayrollInfoDetail_CurrencyDetails_CurrencyId",
                table: "EmployeePayrollInfoDetail",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
