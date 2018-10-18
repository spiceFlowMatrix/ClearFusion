using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DropProfessionalDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_EmployeeDetail_ProfessionDetails_ProfessionId",
            //    table: "EmployeeDetail");

            //migrationBuilder.DropIndex(
            //    name: "IX_EmployeeDetail_ProfessionId",
            //    table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "EmployeeDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "EmployeeDetail",
                nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_EmployeeDetail_ProfessionId",
            //    table: "EmployeeDetail",
            //    column: "ProfessionId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EmployeeDetail_ProfessionDetails_ProfessionId",
            //    table: "EmployeeDetail",
            //    column: "ProfessionId",
            //    principalTable: "ProfessionDetails",
            //    principalColumn: "ProfessionId",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
