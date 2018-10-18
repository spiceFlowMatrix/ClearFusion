using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransfer11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocumentDetail_EmployeeDetail_EmployeeID",
                table: "EmployeeDocumentDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDocumentDetail_EmployeeID",
                table: "EmployeeDocumentDetail");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "EmployeeDocumentDetail",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "DocumentFilePath",
                table: "EmployeeDocumentDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "EmployeeDocumentDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentFilePath",
                table: "EmployeeDocumentDetail");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "EmployeeDocumentDetail");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "EmployeeDocumentDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocumentDetail_EmployeeID",
                table: "EmployeeDocumentDetail",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocumentDetail_EmployeeDetail_EmployeeID",
                table: "EmployeeDocumentDetail",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
