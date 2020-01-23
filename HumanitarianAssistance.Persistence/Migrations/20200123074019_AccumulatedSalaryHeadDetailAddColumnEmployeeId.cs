using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class AccumulatedSalaryHeadDetailAddColumnEmployeeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "AccumulatedSalaryHeadDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccumulatedSalaryHeadDetail_EmployeeId",
                table: "AccumulatedSalaryHeadDetail",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccumulatedSalaryHeadDetail_EmployeeDetail_EmployeeId",
                table: "AccumulatedSalaryHeadDetail",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccumulatedSalaryHeadDetail_EmployeeDetail_EmployeeId",
                table: "AccumulatedSalaryHeadDetail");

            migrationBuilder.DropIndex(
                name: "IX_AccumulatedSalaryHeadDetail_EmployeeId",
                table: "AccumulatedSalaryHeadDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AccumulatedSalaryHeadDetail");
        }
    }
}
