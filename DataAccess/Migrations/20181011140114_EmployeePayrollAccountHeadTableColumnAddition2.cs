using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePayrollAccountHeadTableColumnAddition2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePayrollAccountHead",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeePayrollAccountId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PayrollHeadId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    PayrollHeadTypeId = table.Column<int>(nullable: false),
                    PayrollHeadName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AccountNo = table.Column<long>(nullable: true),
                    TransactionTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayrollAccountHead", x => x.EmployeePayrollAccountId);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollAccountHead_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollAccountHead_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollAccountHead_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollAccountHead_PayrollAccountHead_PayrollHeadId",
                        column: x => x.PayrollHeadId,
                        principalTable: "PayrollAccountHead",
                        principalColumn: "PayrollHeadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollAccountHead_CreatedById",
                table: "EmployeePayrollAccountHead",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollAccountHead_EmployeeId",
                table: "EmployeePayrollAccountHead",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollAccountHead_ModifiedById",
                table: "EmployeePayrollAccountHead",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollAccountHead_PayrollHeadId",
                table: "EmployeePayrollAccountHead",
                column: "PayrollHeadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePayrollAccountHead");
        }
    }
}
