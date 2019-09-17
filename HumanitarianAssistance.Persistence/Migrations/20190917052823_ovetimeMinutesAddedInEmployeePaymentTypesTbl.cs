using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class ovetimeMinutesAddedInEmployeePaymentTypesTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OvertimeMinutes",
                table: "EmployeePaymentTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkingMinutes",
                table: "EmployeePaymentTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OvertimeMinutes",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropColumn(
                name: "WorkingMinutes",
                table: "EmployeePaymentTypes");
        }
    }
}
