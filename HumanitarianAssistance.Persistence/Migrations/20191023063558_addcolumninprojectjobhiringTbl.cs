using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addcolumninprojectjobhiringTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilledVacancies",
                table: "ProjectJobHiringDetail",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JobType",
                table: "ProjectHiringRequestDetail",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilledVacancies",
                table: "ProjectJobHiringDetail");

            migrationBuilder.AlterColumn<int>(
                name: "JobType",
                table: "ProjectHiringRequestDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
