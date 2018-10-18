using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeedetailQualificationRelationDrop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetail_QualificationDetails_QualificationDetailsQualificationId",
                table: "EmployeeDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetail_QualificationDetailsQualificationId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "QualificationDetailsQualificationId",
                table: "EmployeeDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QualificationDetailsQualificationId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_QualificationDetailsQualificationId",
                table: "EmployeeDetail",
                column: "QualificationDetailsQualificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetail_QualificationDetails_QualificationDetailsQualificationId",
                table: "EmployeeDetail",
                column: "QualificationDetailsQualificationId",
                principalTable: "QualificationDetails",
                principalColumn: "QualificationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
