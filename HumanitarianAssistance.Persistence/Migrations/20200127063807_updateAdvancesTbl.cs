using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateAdvancesTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "VoucherReferenceNo",
                table: "Advances",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Advances_ApprovedBy",
                table: "Advances",
                column: "ApprovedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Advances_EmployeeDetail_ApprovedBy",
                table: "Advances",
                column: "ApprovedBy",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advances_EmployeeDetail_ApprovedBy",
                table: "Advances");

            migrationBuilder.DropIndex(
                name: "IX_Advances_ApprovedBy",
                table: "Advances");

            migrationBuilder.AlterColumn<long>(
                name: "VoucherReferenceNo",
                table: "Advances",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
