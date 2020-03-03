using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateInterviewDetalAndTenderBidTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityDate",
                table: "TenderBidSubmission");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                table: "TenderBidSubmission",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DeliveryDateScore",
                table: "TenderBidSubmission",
                nullable: false,
                defaultValue: 0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDateScore",
                table: "TenderBidSubmission");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryDate",
                table: "TenderBidSubmission",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<DateTime>(
                name: "SecurityDate",
                table: "TenderBidSubmission",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
        }
    }
}
