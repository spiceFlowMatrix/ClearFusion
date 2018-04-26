using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class budgetLineEmployeeDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BudgetLineEmployees_EmployeeId",
                table: "BudgetLineEmployees",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetLineEmployees_EmployeeDetail_EmployeeId",
                table: "BudgetLineEmployees",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetLineEmployees_EmployeeDetail_EmployeeId",
                table: "BudgetLineEmployees");

            migrationBuilder.DropIndex(
                name: "IX_BudgetLineEmployees_EmployeeId",
                table: "BudgetLineEmployees");
        }
    }
}
