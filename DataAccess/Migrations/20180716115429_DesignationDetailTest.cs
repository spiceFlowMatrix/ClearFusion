using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DesignationDetailTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContract_DesignationDetail_Designation",
                table: "EmployeeContract",
                column: "Designation",
                principalTable: "DesignationDetail",
                principalColumn: "DesignationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfessionalDetail_DesignationDetail_DesignationId",
                table: "EmployeeProfessionalDetail",
                column: "DesignationId",
                principalTable: "DesignationDetail",
                principalColumn: "DesignationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContract_DesignationDetail_Designation",
                table: "EmployeeContract");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfessionalDetail_DesignationDetail_DesignationId",
                table: "EmployeeProfessionalDetail");
        }
    }
}
