using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class EmployeeContractTypeInProfessionalInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeContractTypeId",
                table: "EmployeeProfessionalDetail",
                type: "int4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_EmployeeContractTypeId",
                table: "EmployeeProfessionalDetail",
                column: "EmployeeContractTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfessionalDetail_EmployeeContractType_EmployeeContractTypeId",
                table: "EmployeeProfessionalDetail",
                column: "EmployeeContractTypeId",
                principalTable: "EmployeeContractType",
                principalColumn: "EmployeeContractTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfessionalDetail_EmployeeContractType_EmployeeContractTypeId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProfessionalDetail_EmployeeContractTypeId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeContractTypeId",
                table: "EmployeeProfessionalDetail");
        }
    }
}
