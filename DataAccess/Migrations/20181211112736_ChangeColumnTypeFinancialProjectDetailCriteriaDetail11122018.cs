using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ChangeColumnTypeFinancialProjectDetailCriteriaDetail11122018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ProjectSelectionId",
                table: "FinancialProjectDetail",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProjectSelectionId",
                table: "FinancialProjectDetail",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
