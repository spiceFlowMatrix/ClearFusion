using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeProfessionalDetailProfessionIdAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "EmployeeProfessionalDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_ProfessionId",
                table: "EmployeeProfessionalDetail",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfessionalDetail_ProfessionDetails_ProfessionId",
                table: "EmployeeProfessionalDetail",
                column: "ProfessionId",
                principalTable: "ProfessionDetails",
                principalColumn: "ProfessionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfessionalDetail_ProfessionDetails_ProfessionId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProfessionalDetail_ProfessionId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "EmployeeProfessionalDetail");
        }
    }
}
