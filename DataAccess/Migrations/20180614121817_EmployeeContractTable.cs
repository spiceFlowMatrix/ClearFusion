using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeContractTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeContract",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeContractId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    FatherName = table.Column<string>(nullable: true),
                    EmployeeCode = table.Column<string>(nullable: true),
                    Designation = table.Column<int>(nullable: false),
                    ContractStartDate = table.Column<DateTime>(nullable: true),
                    ContractEndDate = table.Column<DateTime>(nullable: true),
                    DurationOfContract = table.Column<int>(nullable: false),
                    Salary = table.Column<double>(nullable: true),
                    Grade = table.Column<int>(nullable: true),
                    DutyStation = table.Column<int>(nullable: false),
                    Country = table.Column<int>(nullable: false),
                    Province = table.Column<int>(nullable: false),
                    Project = table.Column<int>(nullable: false),
                    BudgetLine = table.Column<int>(nullable: false),
                    Job = table.Column<string>(nullable: true),
                    WorkTime = table.Column<int>(nullable: false),
                    WorkDayHours = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContract", x => x.EmployeeContractId);
                    table.ForeignKey(
                        name: "FK_EmployeeContract_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeContract_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_CreatedById",
                table: "EmployeeContract",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_ModifiedById",
                table: "EmployeeContract",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeContract");
        }
    }
}
