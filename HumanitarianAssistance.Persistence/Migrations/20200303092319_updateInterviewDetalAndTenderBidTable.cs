using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateInterviewDetalAndTenderBidTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropIndex(
            //     name: "IX_EmployeeBasicSalaryDetail_EmployeeId",
            //     table: "EmployeeBasicSalaryDetail");

            migrationBuilder.RenameColumn(
                name: "SecurityDate",
                table: "TenderBidSubmission",
                newName: "TenderDeliveryDate");

            migrationBuilder.RenameColumn(
                name: "DeliveryDate",
                table: "TenderBidSubmission",
                newName: "DeliveryDateScore");

            migrationBuilder.AlterColumn<double>(
                name: "WrittenTestMarks",
                table: "ProjectInterviewDetails",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "MarksObtained",
                table: "ProjectInterviewDetails",
                nullable: false,
                oldClrType: typeof(int));

            // migrationBuilder.CreateIndex(
            //     name: "IX_EmployeeBasicSalaryDetail_EmployeeId",
            //     table: "EmployeeBasicSalaryDetail",
            //     column: "EmployeeId",
            //     unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropIndex(
            //     name: "IX_EmployeeBasicSalaryDetail_EmployeeId",
            //     table: "EmployeeBasicSalaryDetail");

            migrationBuilder.RenameColumn(
                name: "TenderDeliveryDate",
                table: "TenderBidSubmission",
                newName: "SecurityDate");

            migrationBuilder.RenameColumn(
                name: "DeliveryDateScore",
                table: "TenderBidSubmission",
                newName: "DeliveryDate");

            migrationBuilder.AlterColumn<int>(
                name: "WrittenTestMarks",
                table: "ProjectInterviewDetails",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "MarksObtained",
                table: "ProjectInterviewDetails",
                nullable: false,
                oldClrType: typeof(double));

            // migrationBuilder.CreateIndex(
            //     name: "IX_EmployeeBasicSalaryDetail_EmployeeId",
            //     table: "EmployeeBasicSalaryDetail",
            //     column: "EmployeeId");
        }
    }
}
