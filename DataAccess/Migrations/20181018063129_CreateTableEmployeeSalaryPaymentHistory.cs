using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class CreateTableEmployeeSalaryPaymentHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeSalaryPaymentHistory",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SalaryPaymentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    VoucherNo = table.Column<long>(nullable: false),
                    IsSalaryReversed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryPaymentHistory", x => x.SalaryPaymentId);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryPaymentHistory_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryPaymentHistory_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryPaymentHistory_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryPaymentHistory_VoucherDetail_VoucherNo",
                        column: x => x.VoucherNo,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryPaymentHistory_CreatedById",
                table: "EmployeeSalaryPaymentHistory",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryPaymentHistory_EmployeeId",
                table: "EmployeeSalaryPaymentHistory",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryPaymentHistory_ModifiedById",
                table: "EmployeeSalaryPaymentHistory",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryPaymentHistory_VoucherNo",
                table: "EmployeeSalaryPaymentHistory",
                column: "VoucherNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeSalaryPaymentHistory");
        }
    }
}
