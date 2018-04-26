using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class ForeignKeyEmployeePaymentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentTypes_EmployeeID",
                table: "EmployeePaymentTypes",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePaymentTypes_EmployeeDetail_EmployeeID",
                table: "EmployeePaymentTypes",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePaymentTypes_EmployeeDetail_EmployeeID",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePaymentTypes_EmployeeID",
                table: "EmployeePaymentTypes");
        }
    }
}
